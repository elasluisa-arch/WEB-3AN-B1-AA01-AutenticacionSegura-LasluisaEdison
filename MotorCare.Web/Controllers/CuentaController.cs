using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotorCare.Business;
using MotorCare.Web.Models;

namespace MotorCare.Web.Controllers;

public sealed class CuentaController(IAccountService accountService) : Controller
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Registro()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registro(RegistroViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await accountService.RegisterAsync(model.NombreCompleto, model.Correo, model.Contrasena);
        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult IniciarSesion(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new IniciarSesionViewModel());
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IniciarSesion(IniciarSesionViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await accountService.SignInAsync(model.Correo, model.Contrasena, model.Recordarme);
        if (result.Succeeded)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl!);
            }

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CerrarSesion()
    {
        await accountService.SignOutAsync();
        return RedirectToAction(nameof(IniciarSesion));
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult RecuperarContrasena()
    {
        return View(new RecuperarContrasenaViewModel());
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RecuperarContrasena(RecuperarContrasenaViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await accountService.RequestPasswordResetAsync(
            model.Correo,
            (email, token) => Url.Action(
                nameof(RestablecerContrasena),
                "Cuenta",
                new { correo = email, token },
                protocol: Request.Scheme),
            cancellationToken);

        return RedirectToAction(nameof(RecuperacionSolicitada));
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult RecuperacionSolicitada()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult RestablecerContrasena(string? correo, string? token)
    {
        if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(token))
        {
            return View("EnlaceRecuperacionInvalido");
        }

        return View(new RestablecerContrasenaViewModel { Correo = correo, Token = token });
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RestablecerContrasena(RestablecerContrasenaViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var resetSucceeded = await accountService.ResetPasswordAsync(
            model.Correo,
            model.Token,
            model.NuevaContrasena);
        return resetSucceeded
            ? View("ContrasenaRestablecida")
            : View("EnlaceRecuperacionInvalido");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult AccesoDenegado()
    {
        return View();
    }

    private void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
