using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotorCare.Business;
using MotorCare.Web.Models;

namespace MotorCare.Web.Controllers;

[Authorize]
public sealed class PerfilController(IProfileService profileService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var profile = await profileService.GetProfileAsync(User);
        return profile is null ? Challenge() : View(ToViewModel(profile));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Actualizar(PerfilViewModel model)
    {
        var profile = await profileService.GetProfileAsync(User);
        if (profile is null)
        {
            return Challenge();
        }

        if (!ModelState.IsValid)
        {
            PopulateProfile(model, profile);
            return View("Index", model);
        }

        var result = await profileService.UpdateProfileAsync(User, model.NombreCompleto);
        if (result is null)
        {
            return Challenge();
        }

        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            profile = await profileService.GetProfileAsync(User);
            if (profile is null)
            {
                return Challenge();
            }

            PopulateProfile(model, profile);
            return View("Index", model);
        }

        TempData["MensajePerfil"] = "Tus datos se actualizaron correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarContrasena(CambiarContrasenaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var profile = await profileService.GetProfileAsync(User);
            return profile is null ? Challenge() : View("Index", ToViewModel(profile));
        }

        var result = await profileService.ChangePasswordAsync(User, model.ContrasenaActual, model.NuevaContrasena);
        if (result is null)
        {
            return Challenge();
        }

        if (!result.Succeeded)
        {
            AddIdentityErrors(result, nameof(model.ContrasenaActual));
            var profile = await profileService.GetProfileAsync(User);
            return profile is null ? Challenge() : View("Index", ToViewModel(profile));
        }

        TempData["MensajePerfil"] = "La contraseña se cambió correctamente.";
        return RedirectToAction(nameof(Index));
    }

    private static PerfilViewModel ToViewModel(ProfileSummary profile) => new()
    {
        Correo = profile.Correo,
        FechaRegistro = profile.FechaRegistro,
        NombreCompleto = profile.NombreCompleto
    };

    private static void PopulateProfile(PerfilViewModel model, ProfileSummary profile)
    {
        model.Correo = profile.Correo;
        model.FechaRegistro = profile.FechaRegistro;
    }

    private void AddIdentityErrors(IdentityResult result, string key = "")
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(key, error.Description);
        }
    }
}
