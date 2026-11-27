using System.ComponentModel.DataAnnotations;

namespace MotorCare.Web.Models;

public sealed class IniciarSesionViewModel
{
    [Required(ErrorMessage = "Ingresa tu correo electrónico.")]
    [EmailAddress(ErrorMessage = "Ingresa un correo electrónico válido.")]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingresa tu contraseña.")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Contrasena { get; set; } = string.Empty;

    [Display(Name = "Recordarme")]
    public bool Recordarme { get; set; }
}
