using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Motorcare.Data.Entities;
using MotorCare.Web.Models;

namespace MotorCare.Web.Controllers;

[Authorize]
public sealed class PerfilController(UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await GetCurrentUserAsync();
        if (user is null)
        {
            return Challenge();
        }

        return View(new PerfilViewModel
        {
            Correo = user.Email ?? string.Empty,
            FechaRegistro = user.FechaRegistro,
            NombreCompleto = user.Nombrecompleto
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Actualizar(PerfilViewModel model)
    {
        var user = await GetCurrentUserAsync();
        if (user is null)
        {
            return Challenge();
        }

        if (!ModelState.IsValid)
        {
            model.Correo = user.Email ?? string.Empty;
            model.FechaRegistro = user.FechaRegistro;
            return View("Index", model);
        }

        user.Nombrecompleto = model.NombreCompleto.Trim();
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            model.Correo = user.Email ?? string.Empty;
            model.FechaRegistro = user.FechaRegistro;
            return View("Index", model);
        }

        TempData["MensajePerfil"] = "Tus datos se actualizaron correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarContrasena(CambiarContrasenaViewModel model)
    {
        var user = await GetCurrentUserAsync();
        if (user is null)
        {
            return Challenge();
        }

        if (!ModelState.IsValid)
        {
            return View("Index", await BuildProfileViewModelAsync(user));
        }

        var result = await userManager.ChangePasswordAsync(user, model.ContrasenaActual, model.NuevaContrasena);
        if (!result.Succeeded)
        {
            AddIdentityErrors(result, nameof(model.ContrasenaActual));
            return View("Index", await BuildProfileViewModelAsync(user));
        }

        TempData["MensajePerfil"] = "La contraseña se cambió correctamente.";
        return RedirectToAction(nameof(Index));
    }

    private async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        return await userManager.GetUserAsync(User);
    }

    private static Task<PerfilViewModel> BuildProfileViewModelAsync(ApplicationUser user)
    {
        return Task.FromResult(new PerfilViewModel
        {
            Correo = user.Email ?? string.Empty,
            FechaRegistro = user.FechaRegistro,
            NombreCompleto = user.Nombrecompleto
        });
    }

    private void AddIdentityErrors(IdentityResult result, string key = "")
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(key, error.Description);
        }
    }
}
