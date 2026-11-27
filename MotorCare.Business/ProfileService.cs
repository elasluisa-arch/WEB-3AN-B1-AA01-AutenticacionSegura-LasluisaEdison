using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Motorcare.Data.Entities;

namespace MotorCare.Business;

public interface IProfileService
{
    Task<ProfileSummary?> GetProfileAsync(ClaimsPrincipal principal);
    Task<IdentityResult?> UpdateProfileAsync(ClaimsPrincipal principal, string fullName);
    Task<IdentityResult?> ChangePasswordAsync(ClaimsPrincipal principal, string currentPassword, string newPassword);
}

public sealed class ProfileService(UserManager<ApplicationUser> userManager) : IProfileService
{
    public async Task<ProfileSummary?> GetProfileAsync(ClaimsPrincipal principal)
    {
        var user = await userManager.GetUserAsync(principal);
        return user is null ? null : ToSummary(user);
    }

    public async Task<IdentityResult?> UpdateProfileAsync(ClaimsPrincipal principal, string fullName)
    {
        var user = await userManager.GetUserAsync(principal);
        if (user is null)
        {
            return null;
        }

        user.Nombrecompleto = fullName.Trim();
        return await userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult?> ChangePasswordAsync(
        ClaimsPrincipal principal,
        string currentPassword,
        string newPassword)
    {
        var user = await userManager.GetUserAsync(principal);
        return user is null
            ? null
            : await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    private static ProfileSummary ToSummary(ApplicationUser user) =>
        new(user.Email ?? string.Empty, user.FechaRegistro, user.Nombrecompleto);
}

public sealed record ProfileSummary(string Correo, DateTime FechaRegistro, string NombreCompleto);
