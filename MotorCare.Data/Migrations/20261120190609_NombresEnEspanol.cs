using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorCare.Data.Migrations;

/// <inheritdoc />
public partial class _20261120190609_NombresEnEspanol : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            table: "AspNetRoleClaims");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            table: "AspNetUserClaims");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            table: "AspNetUserLogins");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            table: "AspNetUserRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            table: "AspNetUserRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            table: "AspNetUserTokens");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetUserTokens",
            table: "AspNetUserTokens");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetUsers",
            table: "AspNetUsers");

        migrationBuilder.DropIndex(
            name: "UserNameIndex",
            table: "AspNetUsers");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetUserRoles",
            table: "AspNetUserRoles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetUserLogins",
            table: "AspNetUserLogins");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetUserClaims",
            table: "AspNetUserClaims");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetRoles",
            table: "AspNetRoles");

        migrationBuilder.DropIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AspNetRoleClaims",
            table: "AspNetRoleClaims");

        migrationBuilder.RenameTable(
            name: "AspNetUserTokens",
            newName: "TokensUsuarios");

        migrationBuilder.RenameTable(
            name: "AspNetUsers",
            newName: "Usuarios");

        migrationBuilder.RenameTable(
            name: "AspNetUserRoles",
            newName: "UsuariosRoles");

        migrationBuilder.RenameTable(
            name: "AspNetUserLogins",
            newName: "IniciosSesionUsuarios");

        migrationBuilder.RenameTable(
            name: "AspNetUserClaims",
            newName: "AtributosUsuarios");

        migrationBuilder.RenameTable(
            name: "AspNetRoles",
            newName: "Roles");

        migrationBuilder.RenameTable(
            name: "AspNetRoleClaims",
            newName: "AtributosRoles");

        migrationBuilder.RenameColumn(
            name: "Value",
            table: "TokensUsuarios",
            newName: "Valor");

        migrationBuilder.RenameColumn(
            name: "Name",
            table: "TokensUsuarios",
            newName: "Nombre");

        migrationBuilder.RenameColumn(
            name: "LoginProvider",
            table: "TokensUsuarios",
            newName: "ProveedorInicioSesion");

        migrationBuilder.RenameColumn(
            name: "UserId",
            table: "TokensUsuarios",
            newName: "IdUsuario");

        migrationBuilder.RenameColumn(
            name: "Nombrecompleto",
            table: "Usuarios",
            newName: "NombreCompleto");

        migrationBuilder.RenameColumn(
            name: "UserName",
            table: "Usuarios",
            newName: "NombreUsuario");

        migrationBuilder.RenameColumn(
            name: "TwoFactorEnabled",
            table: "Usuarios",
            newName: "AutenticacionDosFactoresHabilitada");

        migrationBuilder.RenameColumn(
            name: "SecurityStamp",
            table: "Usuarios",
            newName: "SelloSeguridad");

        migrationBuilder.RenameColumn(
            name: "PhoneNumberConfirmed",
            table: "Usuarios",
            newName: "TelefonoConfirmado");

        migrationBuilder.RenameColumn(
            name: "PhoneNumber",
            table: "Usuarios",
            newName: "NumeroTelefono");

        migrationBuilder.RenameColumn(
            name: "PasswordHash",
            table: "Usuarios",
            newName: "HashContrasena");

        migrationBuilder.RenameColumn(
            name: "NormalizedUserName",
            table: "Usuarios",
            newName: "NombreUsuarioNormalizado");

        migrationBuilder.RenameColumn(
            name: "NormalizedEmail",
            table: "Usuarios",
            newName: "CorreoElectronicoNormalizado");

        migrationBuilder.RenameColumn(
            name: "LockoutEnd",
            table: "Usuarios",
            newName: "FinBloqueo");

        migrationBuilder.RenameColumn(
            name: "LockoutEnabled",
            table: "Usuarios",
            newName: "BloqueoHabilitado");

        migrationBuilder.RenameColumn(
            name: "EmailConfirmed",
            table: "Usuarios",
            newName: "CorreoConfirmado");

        migrationBuilder.RenameColumn(
            name: "Email",
            table: "Usuarios",
            newName: "CorreoElectronico");

        migrationBuilder.RenameColumn(
            name: "ConcurrencyStamp",
            table: "Usuarios",
            newName: "SelloConcurrencia");

        migrationBuilder.RenameColumn(
            name: "AccessFailedCount",
            table: "Usuarios",
            newName: "IntentosAccesoFallidos");

        migrationBuilder.RenameColumn(
            name: "Id",
            table: "Usuarios",
            newName: "IdUsuario");

        migrationBuilder.RenameColumn(
            name: "RoleId",
            table: "UsuariosRoles",
            newName: "IdRol");

        migrationBuilder.RenameColumn(
            name: "UserId",
            table: "UsuariosRoles",
            newName: "IdUsuario");

        migrationBuilder.RenameIndex(
            name: "IX_AspNetUserRoles_RoleId",
            table: "UsuariosRoles",
            newName: "IX_UsuariosRoles_IdRol");

        migrationBuilder.RenameColumn(
            name: "UserId",
            table: "IniciosSesionUsuarios",
            newName: "IdUsuario");

        migrationBuilder.RenameColumn(
            name: "ProviderDisplayName",
            table: "IniciosSesionUsuarios",
            newName: "NombreProveedor");

        migrationBuilder.RenameColumn(
            name: "ProviderKey",
            table: "IniciosSesionUsuarios",
            newName: "ClaveProveedor");

        migrationBuilder.RenameColumn(
            name: "LoginProvider",
            table: "IniciosSesionUsuarios",
            newName: "ProveedorInicioSesion");

        migrationBuilder.RenameIndex(
            name: "IX_AspNetUserLogins_UserId",
            table: "IniciosSesionUsuarios",
            newName: "IX_IniciosSesionUsuarios_IdUsuario");

        migrationBuilder.RenameColumn(
            name: "UserId",
            table: "AtributosUsuarios",
            newName: "IdUsuario");

        migrationBuilder.RenameColumn(
            name: "ClaimValue",
            table: "AtributosUsuarios",
            newName: "ValorAtributo");

        migrationBuilder.RenameColumn(
            name: "ClaimType",
            table: "AtributosUsuarios",
            newName: "TipoAtributo");

        migrationBuilder.RenameColumn(
            name: "Id",
            table: "AtributosUsuarios",
            newName: "IdAtributoUsuario");

        migrationBuilder.RenameIndex(
            name: "IX_AspNetUserClaims_UserId",
            table: "AtributosUsuarios",
            newName: "IX_AtributosUsuarios_IdUsuario");

        migrationBuilder.RenameColumn(
            name: "NormalizedName",
            table: "Roles",
            newName: "NombreNormalizado");

        migrationBuilder.RenameColumn(
            name: "Name",
            table: "Roles",
            newName: "Nombre");

        migrationBuilder.RenameColumn(
            name: "ConcurrencyStamp",
            table: "Roles",
            newName: "SelloConcurrencia");

        migrationBuilder.RenameColumn(
            name: "Id",
            table: "Roles",
            newName: "IdRol");

        migrationBuilder.RenameColumn(
            name: "RoleId",
            table: "AtributosRoles",
            newName: "IdRol");

        migrationBuilder.RenameColumn(
            name: "ClaimValue",
            table: "AtributosRoles",
            newName: "ValorAtributo");

        migrationBuilder.RenameColumn(
            name: "ClaimType",
            table: "AtributosRoles",
            newName: "TipoAtributo");

        migrationBuilder.RenameColumn(
            name: "Id",
            table: "AtributosRoles",
            newName: "IdAtributoRol");

        migrationBuilder.RenameIndex(
            name: "IX_AspNetRoleClaims_RoleId",
            table: "AtributosRoles",
            newName: "IX_AtributosRoles_IdRol");

        migrationBuilder.AddPrimaryKey(
            name: "PK_TokensUsuarios",
            table: "TokensUsuarios",
            columns: new[] { "IdUsuario", "ProveedorInicioSesion", "Nombre" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_Usuarios",
            table: "Usuarios",
            column: "IdUsuario");

        migrationBuilder.AddPrimaryKey(
            name: "PK_UsuariosRoles",
            table: "UsuariosRoles",
            columns: new[] { "IdUsuario", "IdRol" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_IniciosSesionUsuarios",
            table: "IniciosSesionUsuarios",
            columns: new[] { "ProveedorInicioSesion", "ClaveProveedor" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_AtributosUsuarios",
            table: "AtributosUsuarios",
            column: "IdAtributoUsuario");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Roles",
            table: "Roles",
            column: "IdRol");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AtributosRoles",
            table: "AtributosRoles",
            column: "IdAtributoRol");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "Usuarios",
            column: "NombreUsuarioNormalizado",
            unique: true,
            filter: "[NombreUsuarioNormalizado] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "Roles",
            column: "NombreNormalizado",
            unique: true,
            filter: "[NombreNormalizado] IS NOT NULL");

        migrationBuilder.AddForeignKey(
            name: "FK_AtributosRoles_Roles_IdRol",
            table: "AtributosRoles",
            column: "IdRol",
            principalTable: "Roles",
            principalColumn: "IdRol",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AtributosUsuarios_Usuarios_IdUsuario",
            table: "AtributosUsuarios",
            column: "IdUsuario",
            principalTable: "Usuarios",
            principalColumn: "IdUsuario",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_IniciosSesionUsuarios_Usuarios_IdUsuario",
            table: "IniciosSesionUsuarios",
            column: "IdUsuario",
            principalTable: "Usuarios",
            principalColumn: "IdUsuario",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_TokensUsuarios_Usuarios_IdUsuario",
            table: "TokensUsuarios",
            column: "IdUsuario",
            principalTable: "Usuarios",
            principalColumn: "IdUsuario",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_UsuariosRoles_Roles_IdRol",
            table: "UsuariosRoles",
            column: "IdRol",
            principalTable: "Roles",
            principalColumn: "IdRol",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_UsuariosRoles_Usuarios_IdUsuario",
            table: "UsuariosRoles",
            column: "IdUsuario",
            principalTable: "Usuarios",
            principalColumn: "IdUsuario",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AtributosRoles_Roles_IdRol",
            table: "AtributosRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_AtributosUsuarios_Usuarios_IdUsuario",
            table: "AtributosUsuarios");

        migrationBuilder.DropForeignKey(
            name: "FK_IniciosSesionUsuarios_Usuarios_IdUsuario",
            table: "IniciosSesionUsuarios");

        migrationBuilder.DropForeignKey(
            name: "FK_TokensUsuarios_Usuarios_IdUsuario",
            table: "TokensUsuarios");

        migrationBuilder.DropForeignKey(
            name: "FK_UsuariosRoles_Roles_IdRol",
            table: "UsuariosRoles");

        migrationBuilder.DropForeignKey(
            name: "FK_UsuariosRoles_Usuarios_IdUsuario",
            table: "UsuariosRoles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_UsuariosRoles",
            table: "UsuariosRoles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Usuarios",
            table: "Usuarios");

        migrationBuilder.DropIndex(
            name: "UserNameIndex",
            table: "Usuarios");

        migrationBuilder.DropPrimaryKey(
            name: "PK_TokensUsuarios",
            table: "TokensUsuarios");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Roles",
            table: "Roles");

        migrationBuilder.DropIndex(
            name: "RoleNameIndex",
            table: "Roles");

        migrationBuilder.DropPrimaryKey(
            name: "PK_IniciosSesionUsuarios",
            table: "IniciosSesionUsuarios");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AtributosUsuarios",
            table: "AtributosUsuarios");

        migrationBuilder.DropPrimaryKey(
            name: "PK_AtributosRoles",
            table: "AtributosRoles");

        migrationBuilder.RenameTable(
            name: "UsuariosRoles",
            newName: "AspNetUserRoles");

        migrationBuilder.RenameTable(
            name: "Usuarios",
            newName: "AspNetUsers");

        migrationBuilder.RenameTable(
            name: "TokensUsuarios",
            newName: "AspNetUserTokens");

        migrationBuilder.RenameTable(
            name: "Roles",
            newName: "AspNetRoles");

        migrationBuilder.RenameTable(
            name: "IniciosSesionUsuarios",
            newName: "AspNetUserLogins");

        migrationBuilder.RenameTable(
            name: "AtributosUsuarios",
            newName: "AspNetUserClaims");

        migrationBuilder.RenameTable(
            name: "AtributosRoles",
            newName: "AspNetRoleClaims");

        migrationBuilder.RenameColumn(
            name: "IdRol",
            table: "AspNetUserRoles",
            newName: "RoleId");

        migrationBuilder.RenameColumn(
            name: "IdUsuario",
            table: "AspNetUserRoles",
            newName: "UserId");

        migrationBuilder.RenameIndex(
            name: "IX_UsuariosRoles_IdRol",
            table: "AspNetUserRoles",
            newName: "IX_AspNetUserRoles_RoleId");

        migrationBuilder.RenameColumn(
            name: "NombreCompleto",
            table: "AspNetUsers",
            newName: "Nombrecompleto");

        migrationBuilder.RenameColumn(
            name: "TelefonoConfirmado",
            table: "AspNetUsers",
            newName: "PhoneNumberConfirmed");

        migrationBuilder.RenameColumn(
            name: "SelloSeguridad",
            table: "AspNetUsers",
            newName: "SecurityStamp");

        migrationBuilder.RenameColumn(
            name: "SelloConcurrencia",
            table: "AspNetUsers",
            newName: "ConcurrencyStamp");

        migrationBuilder.RenameColumn(
            name: "NumeroTelefono",
            table: "AspNetUsers",
            newName: "PhoneNumber");

        migrationBuilder.RenameColumn(
            name: "NombreUsuarioNormalizado",
            table: "AspNetUsers",
            newName: "NormalizedUserName");

        migrationBuilder.RenameColumn(
            name: "NombreUsuario",
            table: "AspNetUsers",
            newName: "UserName");

        migrationBuilder.RenameColumn(
            name: "IntentosAccesoFallidos",
            table: "AspNetUsers",
            newName: "AccessFailedCount");

        migrationBuilder.RenameColumn(
            name: "HashContrasena",
            table: "AspNetUsers",
            newName: "PasswordHash");

        migrationBuilder.RenameColumn(
            name: "FinBloqueo",
            table: "AspNetUsers",
            newName: "LockoutEnd");

        migrationBuilder.RenameColumn(
            name: "CorreoElectronicoNormalizado",
            table: "AspNetUsers",
            newName: "NormalizedEmail");

        migrationBuilder.RenameColumn(
            name: "CorreoElectronico",
            table: "AspNetUsers",
            newName: "Email");

        migrationBuilder.RenameColumn(
            name: "CorreoConfirmado",
            table: "AspNetUsers",
            newName: "EmailConfirmed");

        migrationBuilder.RenameColumn(
            name: "BloqueoHabilitado",
            table: "AspNetUsers",
            newName: "LockoutEnabled");

        migrationBuilder.RenameColumn(
            name: "AutenticacionDosFactoresHabilitada",
            table: "AspNetUsers",
            newName: "TwoFactorEnabled");

        migrationBuilder.RenameColumn(
            name: "IdUsuario",
            table: "AspNetUsers",
            newName: "Id");

        migrationBuilder.RenameColumn(
            name: "Valor",
            table: "AspNetUserTokens",
            newName: "Value");

        migrationBuilder.RenameColumn(
            name: "Nombre",
            table: "AspNetUserTokens",
            newName: "Name");

        migrationBuilder.RenameColumn(
            name: "ProveedorInicioSesion",
            table: "AspNetUserTokens",
            newName: "LoginProvider");

        migrationBuilder.RenameColumn(
            name: "IdUsuario",
            table: "AspNetUserTokens",
            newName: "UserId");

        migrationBuilder.RenameColumn(
            name: "SelloConcurrencia",
            table: "AspNetRoles",
            newName: "ConcurrencyStamp");

        migrationBuilder.RenameColumn(
            name: "NombreNormalizado",
            table: "AspNetRoles",
            newName: "NormalizedName");

        migrationBuilder.RenameColumn(
            name: "Nombre",
            table: "AspNetRoles",
            newName: "Name");

        migrationBuilder.RenameColumn(
            name: "IdRol",
            table: "AspNetRoles",
            newName: "Id");

        migrationBuilder.RenameColumn(
            name: "NombreProveedor",
            table: "AspNetUserLogins",
            newName: "ProviderDisplayName");

        migrationBuilder.RenameColumn(
            name: "IdUsuario",
            table: "AspNetUserLogins",
            newName: "UserId");

        migrationBuilder.RenameColumn(
            name: "ClaveProveedor",
            table: "AspNetUserLogins",
            newName: "ProviderKey");

        migrationBuilder.RenameColumn(
            name: "ProveedorInicioSesion",
            table: "AspNetUserLogins",
            newName: "LoginProvider");

        migrationBuilder.RenameIndex(
            name: "IX_IniciosSesionUsuarios_IdUsuario",
            table: "AspNetUserLogins",
            newName: "IX_AspNetUserLogins_UserId");

        migrationBuilder.RenameColumn(
            name: "ValorAtributo",
            table: "AspNetUserClaims",
            newName: "ClaimValue");

        migrationBuilder.RenameColumn(
            name: "TipoAtributo",
            table: "AspNetUserClaims",
            newName: "ClaimType");

        migrationBuilder.RenameColumn(
            name: "IdUsuario",
            table: "AspNetUserClaims",
            newName: "UserId");

        migrationBuilder.RenameColumn(
            name: "IdAtributoUsuario",
            table: "AspNetUserClaims",
            newName: "Id");

        migrationBuilder.RenameIndex(
            name: "IX_AtributosUsuarios_IdUsuario",
            table: "AspNetUserClaims",
            newName: "IX_AspNetUserClaims_UserId");

        migrationBuilder.RenameColumn(
            name: "ValorAtributo",
            table: "AspNetRoleClaims",
            newName: "ClaimValue");

        migrationBuilder.RenameColumn(
            name: "TipoAtributo",
            table: "AspNetRoleClaims",
            newName: "ClaimType");

        migrationBuilder.RenameColumn(
            name: "IdRol",
            table: "AspNetRoleClaims",
            newName: "RoleId");

        migrationBuilder.RenameColumn(
            name: "IdAtributoRol",
            table: "AspNetRoleClaims",
            newName: "Id");

        migrationBuilder.RenameIndex(
            name: "IX_AtributosRoles_IdRol",
            table: "AspNetRoleClaims",
            newName: "IX_AspNetRoleClaims_RoleId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetUserRoles",
            table: "AspNetUserRoles",
            columns: new[] { "UserId", "RoleId" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetUsers",
            table: "AspNetUsers",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetUserTokens",
            table: "AspNetUserTokens",
            columns: new[] { "UserId", "LoginProvider", "Name" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetRoles",
            table: "AspNetRoles",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetUserLogins",
            table: "AspNetUserLogins",
            columns: new[] { "LoginProvider", "ProviderKey" });

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetUserClaims",
            table: "AspNetUserClaims",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_AspNetRoleClaims",
            table: "AspNetRoleClaims",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "AspNetUsers",
            column: "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId",
            principalTable: "AspNetRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            table: "AspNetUserClaims",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            table: "AspNetUserLogins",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId",
            principalTable: "AspNetRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            table: "AspNetUserRoles",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            table: "AspNetUserTokens",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
