using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Motorcare.Data.Constants;
using Motorcare.Data.Entities;
using MotorCare.Data;
using MotorCare.Web.Models;

namespace MotorCare.Web.Controllers;

[Authorize(Roles = Roles.Administrador)]
public sealed class AdministracionController(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    AppDbContext dbContext) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Usuarios()
    {
        var users = await userManager.Users
            .AsNoTracking()
            .OrderBy(user => user.Email)
            .ToListAsync();

        var viewModels = new List<AdminUsuarioViewModel>(users.Count);
        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            viewModels.Add(new AdminUsuarioViewModel
            {
                Id = user.Id,
                Correo = user.Email ?? string.Empty,
                NombreCompleto = user.Nombrecompleto,
                RolActual = string.Join(", ", roles),
                Rol = roles.FirstOrDefault() ?? Roles.Cliente,
                Habilitado = user.Habilitado
            });
        }

        return View(viewModels);
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View(new AdminCrearUsuarioViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearUsuario(AdminCrearUsuarioViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (!await roleManager.RoleExistsAsync(model.Rol))
        {
            ModelState.AddModelError(nameof(model.Rol), "Selecciona un rol existente.");
            return View(model);
        }

        var correo = model.Correo.Trim();
        var user = new ApplicationUser
        {
            Nombrecompleto = model.NombreCompleto.Trim(),
            UserName = correo,
            Email = correo,
            Habilitado = true,
            FechaRegistro = DateTime.UtcNow
        };

        var createResult = await userManager.CreateAsync(user, model.Contrasena);
        if (!createResult.Succeeded)
        {
            AddIdentityErrors(createResult);
            return View(model);
        }

        var roleResult = await userManager.AddToRoleAsync(user, model.Rol);
        if (!roleResult.Succeeded)
        {
            var deleteResult = await userManager.DeleteAsync(user);
            var roleErrors = string.Join("; ", roleResult.Errors.Select(error => error.Description));
            if (!deleteResult.Succeeded)
            {
                var cleanupErrors = string.Join("; ", deleteResult.Errors.Select(error => error.Description));
                throw new InvalidOperationException(
                    $"No se pudo asignar el rol a la cuenta creada ({roleErrors}) ni eliminar la cuenta incompleta ({cleanupErrors}).");
            }

            AddIdentityErrors(roleResult);
            return View(model);
        }

        TempData["MensajeAdministracion"] = "El usuario se creó correctamente con el rol seleccionado.";
        return RedirectToAction(nameof(Usuarios));
    }

    [HttpGet]
    public async Task<IActionResult> EditarUsuario(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        var roles = await userManager.GetRolesAsync(user);
        return View(new AdminUsuarioViewModel
        {
            Id = user.Id,
            Correo = user.Email ?? string.Empty,
            NombreCompleto = user.Nombrecompleto,
            RolActual = string.Join(", ", roles),
            Rol = roles.FirstOrDefault() ?? Roles.Cliente,
            Habilitado = user.Habilitado
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarUsuario(AdminUsuarioViewModel model)
    {
        if (!ModelState.IsValid || !await roleManager.RoleExistsAsync(model.Rol))
        {
            ModelState.AddModelError(nameof(model.Rol), "Selecciona un rol existente.");
            return View(model);
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var user = await userManager.FindByIdAsync(model.Id);
        if (user is null)
        {
            return NotFound();
        }

        var currentRoles = await userManager.GetRolesAsync(user);
        var isActiveAdministrator = user.Habilitado && currentRoles.Contains(Roles.Administrador);
        var removingAdminAccess = model.Rol != Roles.Administrador || !model.Habilitado;

        if (isActiveAdministrator && removingAdminAccess)
        {
            var activeAdministrators = await userManager.GetUsersInRoleAsync(Roles.Administrador);
            if (activeAdministrators.Count(admin => admin.Habilitado) <= 1)
            {
                ModelState.AddModelError(string.Empty, "No se puede quitar el rol ni deshabilitar al último administrador habilitado.");
                model.Correo = user.Email ?? string.Empty;
                model.NombreCompleto = user.Nombrecompleto;
                model.RolActual = string.Join(", ", currentRoles);
                return View(model);
            }
        }

        var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            await transaction.RollbackAsync();
            AddIdentityErrors(removeResult);
            model.Correo = user.Email ?? string.Empty;
            model.NombreCompleto = user.Nombrecompleto;
            model.RolActual = string.Join(", ", currentRoles);
            return View(model);
        }

        var addRoleResult = await userManager.AddToRoleAsync(user, model.Rol);
        if (!addRoleResult.Succeeded)
        {
            await transaction.RollbackAsync();
            AddIdentityErrors(addRoleResult);
            model.Correo = user.Email ?? string.Empty;
            model.NombreCompleto = user.Nombrecompleto;
            model.RolActual = string.Join(", ", currentRoles);
            return View(model);
        }

        user.Habilitado = model.Habilitado;
        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        
        {
            await transaction.RollbackAsync();
            AddIdentityErrors(updateResult);
            model.Correo = user.Email ?? string.Empty;
            model.NombreCompleto = user.Nombrecompleto;
            model.RolActual = string.Join(", ", currentRoles);
            return View(model);
        }

        await transaction.CommitAsync();
        TempData["MensajeAdministracion"] = "La cuenta se actualizó correctamente.";
        return RedirectToAction(nameof(Usuarios));
    }

    [HttpGet]
    public async Task<IActionResult> RestablecerContrasena(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        if (!await IsFictionalClientAsync(user))
        {
            return Forbid();
        }

        return View(new AdminRestablecerContrasenaViewModel
        {
            Id = user.Id,
            Correo = user.Email ?? string.Empty
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RestablecerContrasena(AdminRestablecerContrasenaViewModel model)
    {
        var user = await userManager.FindByIdAsync(model.Id);
        if (user is null)
        {
            return NotFound();
        }

        if (!await IsFictionalClientAsync(user))
        {
            return Forbid();
        }

        model.Correo = user.Email ?? string.Empty;
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, token, model.NuevaContrasena);
        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            return View(model);
        }

        TempData["MensajeAdministracion"] = "La contraseña de la cuenta ficticia se cambió correctamente.";
        return RedirectToAction(nameof(Usuarios));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarUsuario(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        if (!await IsFictionalClientAsync(user) || user.Id == userManager.GetUserId(User))
        {
            return Forbid();
        }

        var result = await userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(error => error.Description));
            TempData["ErrorAdministracion"] = $"No se pudo eliminar la cuenta ficticia: {errors}";
            return RedirectToAction(nameof(Usuarios));
        }

        TempData["MensajeAdministracion"] = "La cuenta ficticia se eliminó correctamente.";
        return RedirectToAction(nameof(Usuarios));
    }

    private async Task<bool> IsFictionalClientAsync(ApplicationUser user)
    {
        if (user.Email?.EndsWith("@motorcare.test", StringComparison.OrdinalIgnoreCase) != true)
        {
            return false;
        }

        var roles = await userManager.GetRolesAsync(user);
        return roles.Contains(Roles.Cliente) && !roles.Contains(Roles.Administrador);
    }

    private void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
