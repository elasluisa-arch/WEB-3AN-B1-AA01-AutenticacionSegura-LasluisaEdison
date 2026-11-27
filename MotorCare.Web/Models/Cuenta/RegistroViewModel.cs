using System.ComponentModel.DataAnnotations;

namespace MotorCare.Web.Models;

public sealed class RegistroViewModel
{
    [Required(ErrorMessage = "Ingresa tu nombre completo.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    [Display(Name = "Nombre completo")]
    public string NombreCompleto { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingresa tu correo electrónico.")]
    [EmailAddress(ErrorMessage = "Ingresa un correo electrónico válido.")]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingresa una contraseña.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Contrasena { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirma tu contraseña.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas no coinciden.")]
    [Display(Name = "Confirmar contraseña")]
    public string ConfirmarContrasena { get; set; } = string.Empty;
}
