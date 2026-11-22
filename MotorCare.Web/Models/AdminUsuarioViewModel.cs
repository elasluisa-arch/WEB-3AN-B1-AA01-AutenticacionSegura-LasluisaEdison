using System.ComponentModel.DataAnnotations;
using Motorcare.Data.Constants;

namespace MotorCare.Web.Models;

public sealed class AdminUsuarioViewModel
{
    public string Id { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string NombreCompleto { get; set; } = string.Empty;

    public string RolActual { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^(Cliente|Administrador)$", ErrorMessage = "Selecciona un rol válido.")]
    public string Rol { get; set; } = Roles.Cliente;

    [Display(Name = "Cuenta habilitada")]
    public bool Habilitado { get; set; }
}
