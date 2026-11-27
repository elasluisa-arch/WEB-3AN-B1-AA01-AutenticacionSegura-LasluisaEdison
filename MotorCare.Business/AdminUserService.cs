using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Motorcare.Data.Constants;
using Motorcare.Data.Entities;
using MotorCare.Data;

namespace MotorCare.Business;

public interface IAdminUserService
{
    Task<IReadOnlyList<AdminUserSummary>> GetUsersAsync();
    Task<AdminUserSummary?> GetUserAsync(string id);
    Task<IdentityResult> CreateUserAsync(string fullName, string email, string role, string password);
    Task<AdminOperationResult> UpdateUserAsync(string id, string role, bool enabled);
    Task<bool> CanManageFictionalClientAsync(string id);
    Task<AdminOperationResult> ResetFictionalClientPasswordAsync(string id, string newPassword);
    Task<AdminOperationResult> DeleteFictionalClientAsync(string id, string? currentUserId);
}

public sealed class AdminUserService(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    AppDbContext dbContext) : IAdminUserService
{
    public async Task<IReadOnlyList<AdminUserSummary>> GetUsersAsync()
    {
        var users = await userManager.Users
            .AsNoTracking()
            .OrderBy(user => user.Email)
            .ToListAsync();

        var summaries = new List<AdminUserSummary>(users.Count);
        foreach (var user in users)
        {
            summaries.Add(await CreateSummaryAsync(user));
        }

        return summaries;
    }

    public async Task<AdminUserSummary?> GetUserAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        return user is null ? null : await CreateSummaryAsync(user);
    }

    public async Task<IdentityResult> CreateUserAsync(string fullName, string email, string role, string password)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            return IdentityResult.Failed(new IdentityError { Description = "Selecciona un rol existente." });
        }

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

        var roleResult = await userManager.AddToRoleAsync(user, role);
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
        }

        return roleResult;
    }

    public async Task<AdminOperationResult> UpdateUserAsync(string id, string role, bool enabled)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            return AdminOperationResult.Failed("Selecciona un rol existente.");
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return AdminOperationResult.NotFound();
        }

        var currentRoles = await userManager.GetRolesAsync(user);
        var currentSummary = CreateSummary(user, currentRoles);
        var isActiveAdministrator = user.Habilitado && currentRoles.Contains(Roles.Administrador);
        var removingAdminAccess = role != Roles.Administrador || !enabled;

        if (isActiveAdministrator && removingAdminAccess)
        {
            var activeAdministrators = await userManager.GetUsersInRoleAsync(Roles.Administrador);
            if (activeAdministrators.Count(admin => admin.Habilitado) <= 1)
            {
                return AdminOperationResult.LastAdministrator(
                    "No se puede quitar el rol ni deshabilitar al último administrador habilitado.",
                    currentSummary);
            }
        }

        var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            await transaction.RollbackAsync();
            return AdminOperationResult.Failed(removeResult, currentSummary);
        }

        var addRoleResult = await userManager.AddToRoleAsync(user, role);
        if (!addRoleResult.Succeeded)
        {
            await transaction.RollbackAsync();
            return AdminOperationResult.Failed(addRoleResult, currentSummary);
        }

        user.Habilitado = enabled;
        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            await transaction.RollbackAsync();
            return AdminOperationResult.Failed(updateResult, currentSummary);
        }

        await transaction.CommitAsync();
        return AdminOperationResult.Success();
    }

    public async Task<bool> CanManageFictionalClientAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        return user is not null && await IsFictionalClientAsync(user);
    }

    public async Task<AdminOperationResult> ResetFictionalClientPasswordAsync(string id, string newPassword)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return AdminOperationResult.NotFound();
        }

        if (!await IsFictionalClientAsync(user))
        {
            return AdminOperationResult.Forbidden();
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var result = await userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded
            ? AdminOperationResult.Success()
            : AdminOperationResult.Failed(result);
    }

    public async Task<AdminOperationResult> DeleteFictionalClientAsync(string id, string? currentUserId)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return AdminOperationResult.NotFound();
        }

        if (!await IsFictionalClientAsync(user) || user.Id == currentUserId)
        {
            return AdminOperationResult.Forbidden();
        }

        var result = await userManager.DeleteAsync(user);
        return result.Succeeded
            ? AdminOperationResult.Success()
            : AdminOperationResult.Failed(result);
    }

    private async Task<AdminUserSummary> CreateSummaryAsync(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        return CreateSummary(user, roles);
    }

    private static AdminUserSummary CreateSummary(ApplicationUser user, IEnumerable<string> roles)
    {
        var roleList = roles.ToList();
        return new AdminUserSummary(
            user.Id,
            user.Email ?? string.Empty,
            user.Nombrecompleto,
            string.Join(", ", roleList),
            roleList.FirstOrDefault() ?? Roles.Cliente,
            user.Habilitado);
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
}

public sealed record AdminUserSummary(
    string Id,
    string Correo,
    string NombreCompleto,
    string RolActual,
    string Rol,
    bool Habilitado);

public enum AdminOperationStatus
{
    Succeeded,
    Failed,
    NotFound,
    Forbidden,
    LastAdministrator
}

public sealed record AdminOperationResult(
    AdminOperationStatus Status,
    IReadOnlyList<string> Errors,
    AdminUserSummary? User = null)
{
    public static AdminOperationResult Success() => new(AdminOperationStatus.Succeeded, []);

    public static AdminOperationResult Failed(params string[] errors) =>
        new(AdminOperationStatus.Failed, errors);

    public static AdminOperationResult Failed(IdentityResult result, AdminUserSummary? user = null) =>
        new(AdminOperationStatus.Failed, result.Errors.Select(error => error.Description).ToArray(), user);

    public static AdminOperationResult NotFound() => new(AdminOperationStatus.NotFound, []);

    public static AdminOperationResult Forbidden() => new(AdminOperationStatus.Forbidden, []);

    public static AdminOperationResult LastAdministrator(string error, AdminUserSummary user) =>
        new(AdminOperationStatus.LastAdministrator, [error], user);
}
