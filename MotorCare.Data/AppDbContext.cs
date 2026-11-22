using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Motorcare.Data.Entities;

namespace MotorCare.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(e =>
            {
                e.ToTable("Usuarios");
                e.Property(u => u.Id).HasColumnName("IdUsuario");
                e.Property(u => u.Nombrecompleto).HasColumnName("NombreCompleto").HasMaxLength(100);
                e.Property(u => u.UserName).HasColumnName("NombreUsuario");
                e.Property(u => u.NormalizedUserName).HasColumnName("NombreUsuarioNormalizado");
                e.Property(u => u.Email).HasColumnName("CorreoElectronico");
                e.Property(u => u.NormalizedEmail).HasColumnName("CorreoElectronicoNormalizado");
                e.Property(u => u.EmailConfirmed).HasColumnName("CorreoConfirmado");
                e.Property(u => u.PasswordHash).HasColumnName("HashContrasena");
                e.Property(u => u.SecurityStamp).HasColumnName("SelloSeguridad");
                e.Property(u => u.ConcurrencyStamp).HasColumnName("SelloConcurrencia");
                e.Property(u => u.PhoneNumber).HasColumnName("NumeroTelefono");
                e.Property(u => u.PhoneNumberConfirmed).HasColumnName("TelefonoConfirmado");
                e.Property(u => u.TwoFactorEnabled).HasColumnName("AutenticacionDosFactoresHabilitada");
                e.Property(u => u.LockoutEnd).HasColumnName("FinBloqueo");
                e.Property(u => u.LockoutEnabled).HasColumnName("BloqueoHabilitado");
                e.Property(u => u.AccessFailedCount).HasColumnName("IntentosAccesoFallidos");
            });

            builder.Entity<IdentityRole>(e =>
            {
                e.ToTable("Roles");
                e.Property(r => r.Id).HasColumnName("IdRol");
                e.Property(r => r.Name).HasColumnName("Nombre");
                e.Property(r => r.NormalizedName).HasColumnName("NombreNormalizado");
                e.Property(r => r.ConcurrencyStamp).HasColumnName("SelloConcurrencia");
            });

            builder.Entity<IdentityRoleClaim<string>>(e =>
            {
                e.ToTable("AtributosRoles");
                e.Property(c => c.Id).HasColumnName("IdAtributoRol");
                e.Property(c => c.RoleId).HasColumnName("IdRol");
                e.Property(c => c.ClaimType).HasColumnName("TipoAtributo");
                e.Property(c => c.ClaimValue).HasColumnName("ValorAtributo");
            });

            builder.Entity<IdentityUserClaim<string>>(e =>
            {
                e.ToTable("AtributosUsuarios");
                e.Property(c => c.Id).HasColumnName("IdAtributoUsuario");
                e.Property(c => c.UserId).HasColumnName("IdUsuario");
                e.Property(c => c.ClaimType).HasColumnName("TipoAtributo");
                e.Property(c => c.ClaimValue).HasColumnName("ValorAtributo");
            });

            builder.Entity<IdentityUserLogin<string>>(e =>
            {
                e.ToTable("IniciosSesionUsuarios");
                e.Property(l => l.UserId).HasColumnName("IdUsuario");
                e.Property(l => l.LoginProvider).HasColumnName("ProveedorInicioSesion");
                e.Property(l => l.ProviderKey).HasColumnName("ClaveProveedor");
                e.Property(l => l.ProviderDisplayName).HasColumnName("NombreProveedor");
            });

            builder.Entity<IdentityUserRole<string>>(e =>
            {
                e.ToTable("UsuariosRoles");
                e.Property(r => r.UserId).HasColumnName("IdUsuario");
                e.Property(r => r.RoleId).HasColumnName("IdRol");
            });

            builder.Entity<IdentityUserToken<string>>(e =>
            {
                e.ToTable("TokensUsuarios");
                e.Property(t => t.UserId).HasColumnName("IdUsuario");
                e.Property(t => t.LoginProvider).HasColumnName("ProveedorInicioSesion");
                e.Property(t => t.Name).HasColumnName("Nombre");
                e.Property(t => t.Value).HasColumnName("Valor");
            });
        }
    }
}