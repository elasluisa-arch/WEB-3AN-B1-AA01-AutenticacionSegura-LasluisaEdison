using System.ComponentModel.DataAnnotations;

namespace MotorCare.Web.Models;

public sealed class PerfilViewModel
{
    public string Correo { get; set; } = string.Empty;

    public DateTime FechaRegistro { get; set; }

    [Required(ErrorMessage = "Ingresa tu nombre completo.")]
    
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    
    [Display(Name = "Nombre completo")]
    
    public string NombreCompleto { get; set; } = string.Empty;
}
