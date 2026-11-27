using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Motorcare.Data.Constants;
using MotorCare.Business;
using MotorCare.Web.Models;

namespace MotorCare.Web.Controllers;

[Authorize(Roles = Roles.Administrador)]
public sealed class AdministracionController(IAdminUserService adminUserService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Usuarios()
    {
        var users = await adminUserService.GetUsersAsync();
        return View(users.Select(ToViewModel).ToList());
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

        var result = await adminUserService.CreateUserAsync(
            model.NombreCompleto,
            model.Correo,
            model.Rol,
            model.Contrasena);
        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            return View(model);
        }

        TempData["MensajeAdministracion"] = "El usuario se creó correctamente con el rol seleccionado.";
        return RedirectToAction(nameof(Usuarios));
    }

    [HttpGet]
    public async Task<IActionResult> EditarUsuario(string id)
    {
        var user = await adminUserService.GetUserAsync(id);
        return user is null ? NotFound() : View(ToViewModel(user));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarUsuario(AdminUsuarioViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await adminUserService.UpdateUserAsync(model.Id, model.Rol, model.Habilitado);
        if (result.Status == AdminOperationStatus.NotFound)
        {
            return NotFound();
        }

        if (result.Status == AdminOperationStatus.Forbidden)
        {
            return Forbid();
        }

        if (result.Status != AdminOperationStatus.Succeeded)
        {
            AddErrors(result.Errors);
            if (result.User is not null)
            {
                PopulateCurrentUser(model, result.User);
            }

            return View(model);
        }

        TempData["MensajeAdministracion"] = "La cuenta se actualizó correctamente.";
        return RedirectToAction(nameof(Usuarios));
    }

    [HttpGet]
    public async Task<IActionResult> RestablecerContrasena(string id)
    {
        var user = await adminUserService.GetUserAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        if (!await adminUserService.CanManageFictionalClientAsync(id))
        {
            return Forbid();
        }

        return View(new AdminRestablecerContrasenaViewModel
        {
            Id = user.Id,
            Correo = user.Correo
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RestablecerContrasena(AdminRestablecerContrasenaViewModel model)
    {
        var user = await adminUserService.GetUserAsync(model.Id);
        if (user is null)
        {
            return NotFound();
        }

        if (!await adminUserService.CanManageFictionalClientAsync(model.Id))
        {
            return Forbid();
        }

        model.Correo = user.Correo;
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await adminUserService.ResetFictionalClientPasswordAsync(model.Id, model.NuevaContrasena);
        if (result.Status == AdminOperationStatus.NotFound)
        {
            return NotFound();
        }

        if (result.Status == AdminOperationStatus.Forbidden)
        {
            return Forbid();
        }

        if (result.Status != AdminOperationStatus.Succeeded)
        {
            AddErrors(result.Errors);
            return View(model);
        }

        TempData["MensajeAdministracion"] = "La contraseña de la cuenta ficticia se cambió correctamente.";
        return RedirectToAction(nameof(Usuarios));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarUsuario(string id)
    {
        var result = await adminUserService.DeleteFictionalClientAsync(
            id,
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        if (result.Status == AdminOperationStatus.NotFound)
        {
            return NotFound();
        }

        if (result.Status == AdminOperationStatus.Forbidden)
        {
            return Forbid();
        }

        if (result.Status != AdminOperationStatus.Succeeded)
        {
            TempData["ErrorAdministracion"] = $"No se pudo eliminar la cuenta ficticia: {string.Join("; ", result.Errors)}";
            return RedirectToAction(nameof(Usuarios));
        }

        TempData["MensajeAdministracion"] = "La cuenta ficticia se eliminó correctamente.";
        return RedirectToAction(nameof(Usuarios));
    }

    private static AdminUsuarioViewModel ToViewModel(AdminUserSummary user) => new()
    {
        Id = user.Id,
        Correo = user.Correo,
        NombreCompleto = user.NombreCompleto,
        RolActual = user.RolActual,
        Rol = user.Rol,
        Habilitado = user.Habilitado
    };

    private static void PopulateCurrentUser(AdminUsuarioViewModel model, AdminUserSummary user)
    {
        model.Correo = user.Correo;
        model.NombreCompleto = user.NombreCompleto;
        model.RolActual = user.RolActual;
    }

    private void AddIdentityErrors(IdentityResult result) => AddErrors(result.Errors.Select(error => error.Description));

    private void AddErrors(IEnumerable<string> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(string.Empty, error);
        }
    }
}
