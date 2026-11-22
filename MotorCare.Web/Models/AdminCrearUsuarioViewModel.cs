using System.ComponentModel.DataAnnotations;
using Motorcare.Data.Constants;

namespace MotorCare.Web.Models;

public sealed class AdminCrearUsuarioViewModel
{
    [Required(ErrorMessage = "Ingresa el nombre completo.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    [Display(Name = "Nombre completo")]
    public string NombreCompleto { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingresa el correo electrónico.")]
    [EmailAddress(ErrorMessage = "Ingresa un correo electrónico válido.")]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Selecciona un rol.")]
    [RegularExpression("^(Cliente|Administrador)$", ErrorMessage = "Selecciona un rol válido.")]
    public string Rol { get; set; } = Roles.Cliente;

    [Required(ErrorMessage = "Ingresa una contraseña inicial.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña inicial")]
    public string Contrasena { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirma la contraseña inicial.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas no coinciden.")]
    [Display(Name = "Confirmar contraseña")]
    public string ConfirmarContrasena { get; set; } = string.Empty;
}
