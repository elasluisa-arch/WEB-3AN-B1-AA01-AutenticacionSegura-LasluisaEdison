using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Motorcare.Data.Constants;
using Motorcare.Data.Entities;

namespace MotorCare.Business;

public interface IAccountService
{
    Task<IdentityResult> RegisterAsync(string fullName, string email, string password);
    Task<SignInResult> SignInAsync(string email, string password, bool rememberMe);
    Task SignOutAsync();
    Task RequestPasswordResetAsync(
        string email,
        Func<string, string, string?> createResetUrl,
        CancellationToken cancellationToken);
    Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
}

public sealed class AccountService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IEmailService emailService,
    ILogger<AccountService> logger) : IAccountService
{
    public async Task<IdentityResult> RegisterAsync(string fullName, string email, string password)
    {
        var normalizedEmail = email.Trim();
        var user = new ApplicationUser
        {
            Nombrecompleto = fullName.Trim(),
            UserName = normalizedEmail,
            Email = normalizedEmail,
            Habilitado = true,
            FechaRegistro = DateTime.UtcNow
        };

        var createResult = await userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
        {
            return createResult;
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
        return IdentityResult.Success;
    }

    public async Task<SignInResult> SignInAsync(string email, string password, bool rememberMe)
    {
        var user = await userManager.FindByEmailAsync(email.Trim());
        if (user is null || !user.Habilitado)
        {
            return SignInResult.Failed;
        }

        var result = await signInManager.PasswordSignInAsync(
            user,
            password,
            rememberMe,
            lockoutOnFailure: true);

        if (result.IsLockedOut)
        {
            logger.LogWarning("Se rechazó el inicio de sesión porque la cuenta {UserId} está bloqueada.", user.Id);
        }

        return result;
    }

    public Task SignOutAsync() => signInManager.SignOutAsync();

    public async Task RequestPasswordResetAsync(
        string email,
        Func<string, string, string?> createResetUrl,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(email.Trim());
        if (user is null || !user.Habilitado || string.IsNullOrWhiteSpace(user.Email))
        {
            return;
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetUrl = createResetUrl(user.Email, token);
        if (string.IsNullOrWhiteSpace(resetUrl))
        {
            throw new InvalidOperationException("No se pudo generar el enlace de recuperación.");
        }

        var safeName = HtmlEncoder.Default.Encode(user.Nombrecompleto);
        var safeUrl = HtmlEncoder.Default.Encode(resetUrl);
        var htmlBody = $"<p>Hola " +
            $"{safeName},</p><p>Para restablecer tu contraseña de MotorCare, abre este enlace:</p><p><a href=\"{safeUrl}\">Restablecer contraseña</a></p><p>El enlace vence en dos horas. Si no solicitaste este cambio, ignora este mensaje.</p>";
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
        catch (Exception exception) when (exception is EmailDeliveryException
            or InvalidOperationException
            or FormatException)
        {
            logger.LogError(exception, "No se pudo enviar el correo de recuperación para la cuenta {UserId}.", user.Id);
        }
    }

    public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await userManager.FindByEmailAsync(email.Trim());
        if (user is null || !user.Habilitado)
        {
            return false;
        }

        var result = await userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }
}
