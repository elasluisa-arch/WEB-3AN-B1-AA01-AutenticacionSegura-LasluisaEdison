using System.ComponentModel.DataAnnotations;

namespace MotorCare.Web.Models;

public sealed class RecuperarContrasenaViewModel
{
    [Required(ErrorMessage = "Ingresa tu correo electrónico.")]
    [EmailAddress(ErrorMessage = "Ingresa un correo electrónico válido.")]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = string.Empty;
}
