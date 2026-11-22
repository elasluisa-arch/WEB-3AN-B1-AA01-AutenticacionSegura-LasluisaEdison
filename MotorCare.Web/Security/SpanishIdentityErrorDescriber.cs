using Microsoft.AspNetCore.Identity;

namespace MotorCare.Web.Security;

public sealed class SpanishIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError PasswordMismatch() =>
        Create(nameof(PasswordMismatch), "La contraseña actual es incorrecta.");

    public override IdentityError DuplicateUserName(string userName) =>
        Create(nameof(DuplicateUserName), "Ese correo o nombre de usuario ya está registrado.");

    public override IdentityError DuplicateEmail(string email) =>
        Create(nameof(DuplicateEmail), "Ese correo electrónico ya está registrado.");

    public override IdentityError PasswordTooShort(int length) =>
        Create(nameof(PasswordTooShort), $"La contraseña debe tener al menos {length} caracteres.");

    public override IdentityError PasswordRequiresNonAlphanumeric() =>
        Create(nameof(PasswordRequiresNonAlphanumeric), "La contraseña debe incluir al menos un símbolo.");

    public override IdentityError PasswordRequiresDigit() =>
        Create(nameof(PasswordRequiresDigit), "La contraseña debe incluir al menos un número.");

    public override IdentityError PasswordRequiresLower() =>
        Create(nameof(PasswordRequiresLower), "La contraseña debe incluir al menos una letra minúscula.");

    public override IdentityError PasswordRequiresUpper() =>
        Create(nameof(PasswordRequiresUpper), "La contraseña debe incluir al menos una letra mayúscula.");

    private static IdentityError Create(string code, string description) => new()
    {
        Code = code,
        Description = description
    };
}
