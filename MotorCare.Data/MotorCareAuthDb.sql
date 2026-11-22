
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [Nombrecompleto] nvarchar(100) NOT NULL,
        [Habilitado] bit NOT NULL,
        [FechaRegistro] datetime2 NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120185613_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261120185613_InitialCreate', N'11.0.0');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetUserTokens] DROP CONSTRAINT [PK_AspNetUserTokens];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [PK_AspNetUsers];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    DROP INDEX [UserNameIndex] ON [AspNetUsers];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [PK_AspNetUserRoles];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [PK_AspNetUserLogins];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [PK_AspNetUserClaims];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetRoles] DROP CONSTRAINT [PK_AspNetRoles];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    DROP INDEX [RoleNameIndex] ON [AspNetRoles];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [PK_AspNetRoleClaims];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AspNetUserTokens]', N'TokensUsuarios', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AspNetUsers]', N'Usuarios', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AspNetUserRoles]', N'UsuariosRoles', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AspNetUserLogins]', N'IniciosSesionUsuarios', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AspNetUserClaims]', N'AtributosUsuarios', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AspNetRoles]', N'Roles', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AspNetRoleClaims]', N'AtributosRoles', 'OBJECT';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[TokensUsuarios].[Value]', N'Valor', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[TokensUsuarios].[Name]', N'Nombre', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[TokensUsuarios].[LoginProvider]', N'ProveedorInicioSesion', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[TokensUsuarios].[UserId]', N'IdUsuario', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[Nombrecompleto]', N'NombreCompleto', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[UserName]', N'NombreUsuario', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[TwoFactorEnabled]', N'AutenticacionDosFactoresHabilitada', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[SecurityStamp]', N'SelloSeguridad', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[PhoneNumberConfirmed]', N'TelefonoConfirmado', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[PhoneNumber]', N'NumeroTelefono', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[PasswordHash]', N'HashContrasena', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[NormalizedUserName]', N'NombreUsuarioNormalizado', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[NormalizedEmail]', N'CorreoElectronicoNormalizado', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[LockoutEnd]', N'FinBloqueo', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[LockoutEnabled]', N'BloqueoHabilitado', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[EmailConfirmed]', N'CorreoConfirmado', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[Email]', N'CorreoElectronico', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[ConcurrencyStamp]', N'SelloConcurrencia', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[AccessFailedCount]', N'IntentosAccesoFallidos', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Usuarios].[Id]', N'IdUsuario', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[UsuariosRoles].[RoleId]', N'IdRol', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[UsuariosRoles].[UserId]', N'IdUsuario', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[UsuariosRoles].[IX_AspNetUserRoles_RoleId]', N'IX_UsuariosRoles_IdRol', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[IniciosSesionUsuarios].[UserId]', N'IdUsuario', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[IniciosSesionUsuarios].[ProviderDisplayName]', N'NombreProveedor', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[IniciosSesionUsuarios].[ProviderKey]', N'ClaveProveedor', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[IniciosSesionUsuarios].[LoginProvider]', N'ProveedorInicioSesion', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[IniciosSesionUsuarios].[IX_AspNetUserLogins_UserId]', N'IX_IniciosSesionUsuarios_IdUsuario', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AtributosUsuarios].[UserId]', N'IdUsuario', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AtributosUsuarios].[ClaimValue]', N'ValorAtributo', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AtributosUsuarios].[ClaimType]', N'TipoAtributo', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AtributosUsuarios].[Id]', N'IdAtributoUsuario', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AtributosUsuarios].[IX_AspNetUserClaims_UserId]', N'IX_AtributosUsuarios_IdUsuario', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Roles].[NormalizedName]', N'NombreNormalizado', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Roles].[Name]', N'Nombre', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Roles].[ConcurrencyStamp]', N'SelloConcurrencia', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[Roles].[Id]', N'IdRol', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AtributosRoles].[RoleId]', N'IdRol', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AtributosRoles].[ClaimValue]', N'ValorAtributo', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AtributosRoles].[ClaimType]', N'TipoAtributo', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AtributosRoles].[Id]', N'IdAtributoRol', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC sp_rename N'[AtributosRoles].[IX_AspNetRoleClaims_RoleId]', N'IX_AtributosRoles_IdRol', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [TokensUsuarios] ADD CONSTRAINT [PK_TokensUsuarios] PRIMARY KEY ([IdUsuario], [ProveedorInicioSesion], [Nombre]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [Usuarios] ADD CONSTRAINT [PK_Usuarios] PRIMARY KEY ([IdUsuario]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [UsuariosRoles] ADD CONSTRAINT [PK_UsuariosRoles] PRIMARY KEY ([IdUsuario], [IdRol]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [IniciosSesionUsuarios] ADD CONSTRAINT [PK_IniciosSesionUsuarios] PRIMARY KEY ([ProveedorInicioSesion], [ClaveProveedor]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AtributosUsuarios] ADD CONSTRAINT [PK_AtributosUsuarios] PRIMARY KEY ([IdAtributoUsuario]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [Roles] ADD CONSTRAINT [PK_Roles] PRIMARY KEY ([IdRol]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AtributosRoles] ADD CONSTRAINT [PK_AtributosRoles] PRIMARY KEY ([IdAtributoRol]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Usuarios] ([NombreUsuarioNormalizado]) WHERE [NombreUsuarioNormalizado] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NombreNormalizado]) WHERE [NombreNormalizado] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AtributosRoles] ADD CONSTRAINT [FK_AtributosRoles_Roles_IdRol] FOREIGN KEY ([IdRol]) REFERENCES [Roles] ([IdRol]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [AtributosUsuarios] ADD CONSTRAINT [FK_AtributosUsuarios_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([IdUsuario]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [IniciosSesionUsuarios] ADD CONSTRAINT [FK_IniciosSesionUsuarios_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([IdUsuario]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [TokensUsuarios] ADD CONSTRAINT [FK_TokensUsuarios_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([IdUsuario]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [UsuariosRoles] ADD CONSTRAINT [FK_UsuariosRoles_Roles_IdRol] FOREIGN KEY ([IdRol]) REFERENCES [Roles] ([IdRol]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    ALTER TABLE [UsuariosRoles] ADD CONSTRAINT [FK_UsuariosRoles_Usuarios_IdUsuario] FOREIGN KEY ([IdUsuario]) REFERENCES [Usuarios] ([IdUsuario]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261120190609_NombresEnEspanol'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261120190609_NombresEnEspanol', N'11.0.0');
END;

COMMIT;
GO

