using Microsoft.AspNetCore.Identity;

namespace Motorcare.Data.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string Nombrecompleto { get; set; }
		public bool Habilitado { get; set; } = true;
		public DateTime FechaRegistro { get; set; } = DateTime.Now;
	}
}