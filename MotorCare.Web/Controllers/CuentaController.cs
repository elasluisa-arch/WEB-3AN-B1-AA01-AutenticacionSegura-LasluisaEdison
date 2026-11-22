using System.IO;
using System.Security.Authentication;
using System.Text.Encodings.Web;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Motorcare.Data.Constants;
using Motorcare.Data.Entities;
using MotorCare.Business;
using MotorCare.Web.Models;

namespace MotorCare.Web.Controllers;

public sealed class CuentaController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IEmailService emailService,
    ILogger<CuentaController> logger) : Controller
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

        var user = new ApplicationUser
        {
            Nombrecompleto = model.NombreCompleto.Trim(),
            UserName = model.Correo.Trim(),
            Email = model.Correo.Trim(),
            Habilitado = true,
            FechaRegistro = DateTime.UtcNow
        };

        var createResult = await userManager.CreateAsync(user, model.Contrasena);
        if (!createResult.Succeeded)
        {
            foreach (var error in createResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        var roleResult = await userManager.AddToRoleAsync(user, Roles.Cliente);
        if (!roleResult.Succeeded)
        {
            var deleteResult = await userManager.DeleteAsync(user);
            var roleError = string.Join("; ", roleResult.Errors.Select(error => error.Description));
            var cleanupError = deleteResult.Succeeded
                ? string.Empty
                : $" Además, no se pudo eliminar la cuenta incompleta: {string.Join("; ", deleteResult.Errors.Select(error => error.Description))}";

            throw new InvalidOperationException(
                $"No se pudo asignar el rol Cliente a la cuenta registrada. {roleError}{cleanupError}");
        }

        await signInManager.SignInAsync(user, isPersistent: false);
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

        var user = await userManager.FindByEmailAsync(model.Correo.Trim());
        if (user is null || !user.Habilitado)
        {
            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            return View(model);
        }

        var result = await signInManager.PasswordSignInAsync(
            user,
            model.Contrasena,
            model.Recordarme,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl!);
            }

            return RedirectToAction("Index", "Home");
        }

        if (result.IsLockedOut)
        {
            logger.LogWarning("Se rechazó el inicio de sesión porque la cuenta {UserId} está bloqueada.", user.Id);
            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            return View(model);
        }

        ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CerrarSesion()
    {
        await signInManager.SignOutAsync();
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

        var user = await userManager.FindByEmailAsync(model.Correo.Trim());
        if (user is not null && user.Habilitado && !string.IsNullOrWhiteSpace(user.Email))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetUrl = Url.Action(
                nameof(RestablecerContrasena),
                "Cuenta",
                new { correo = user.Email, token },
                protocol: Request.Scheme);

            if (string.IsNullOrWhiteSpace(resetUrl))
            {
                throw new InvalidOperationException("No se pudo generar el enlace de recuperación.");
            }

            var safeName = HtmlEncoder.Default.Encode(user.Nombrecompleto);
            var safeUrl = HtmlEncoder.Default.Encode(resetUrl);
            var htmlBody = $"<p>Hola {safeName},</p><p>Para restablecer tu contraseña de MotorCare, abre este enlace:</p><p><a href=\"{safeUrl}\">Restablecer contraseña</a></p><p>El enlace vence en dos horas. Si no solicitaste este cambio, ignora este mensaje.</p>";
            const string textBody = "Recibimos una solicitud para restablecer tu contraseña de MotorCare. Usa el enlace de este correo. El enlace vence en dos horas. Si no solicitaste este cambio, ignora este mensaje.";

            try
            {
                await emailService.SendAsync(
                    user.Email,
                    "Restablecer contraseña de MotorCare",
                    htmlBody,
                    textBody,
                    cancellationToken);
            }
            catch (Exception exception) when (exception is MailKit.ProtocolException
                or MailKit.CommandException
                or MailKit.ServiceNotConnectedException
                or MailKit.ServiceNotAuthenticatedException
                or AuthenticationException
                or IOException
                or InvalidOperationException
                or FormatException)
            {
                logger.LogError(exception, "No se pudo enviar el correo de recuperación para la cuenta {UserId}.", user.Id);
            }
        }

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

        var user = await userManager.FindByEmailAsync(model.Correo.Trim());
        if (user is null || !user.Habilitado)
        {
            return View("EnlaceRecuperacionInvalido");
        }

        var result = await userManager.ResetPasswordAsync(user, model.Token, model.NuevaContrasena);
        if (!result.Succeeded)
        {
            return View("EnlaceRecuperacionInvalido");
        }

        return View("ContrasenaRestablecida");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult AccesoDenegado()
    {
        return View();
    }
}
