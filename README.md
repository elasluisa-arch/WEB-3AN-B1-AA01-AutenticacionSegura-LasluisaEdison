# MotorCare - autenticación segura

Módulo de autenticación para Ensigma Taller Mecánico, desarrollado con ASP.NET Core MVC, ASP.NET Core Identity, Entity Framework Core y SQL Server.

## Requisitos

- Visual Studio con el SDK de .NET 11 compatible con el proyecto.
- SQL Server (la configuración local inicial usa la instancia `edison`).
- Una cuenta SMTP real para probar recuperación de contraseña.

## Proyectos

- `MotorCare.Web`: MVC, vistas, autenticación, cookies y autorización.
- `MotorCare.Business`: contratos de servicios de negocio, incluido `IEmailService`.
- `MotorCare.Data`: Identity, `AppDbContext`, entidades y migraciones.
- `MotorCare.Infrastructure`: implementación SMTP con MailKit.

## Preparar SQL Server

La cadena local está en `MotorCare.Web/appsettings.json` y apunta a `MotorCareAuthDb`. Ajusta únicamente el servidor/base para tu entorno; no guardes contraseñas ni secretos en ese archivo.

Hay dos formas de preparar las tablas:

1. En Visual Studio, establece `MotorCare.Web` como proyecto de inicio y ejecuta `Update-Database` desde la Consola del Administrador de paquetes, con `MotorCare.Data` como proyecto de migración.
2. Crea/selecciona la base `MotorCareAuthDb` en SQL Server Management Studio y ejecuta `MotorCare.Data/MotorCareAuthDb.sql` sobre esa base. El script es idempotente y procede de las migraciones incluidas.

Las migraciones también crean y renombran tablas y columnas de ASP.NET Identity en español. Al iniciar, la aplicación garantiza los roles `Cliente` y `Administrador`.

## Configuración privada con Secretos de usuario

En Visual Studio, clic derecho sobre `MotorCare.Web` → **Administrar secretos de usuario**. Los secretos son locales y no deben copiarse a `appsettings.json`, al repositorio ni a capturas compartidas.

Configuración de ejemplo (reemplaza los valores localmente; no guardes contraseñas reales en este README):

```json
{
  "Smtp": {
	"Host": "smtp.gmail.com",
	"Port": 587,
	"UserName": "tu-cuenta@gmail.com",
	"Password": "CONTRASENA_DE_APLICACION",
	"FromAddress": "tu-cuenta@gmail.com",
	"FromName": "MotorCare"
  },
  "AdminSeed": {
	"Email": "correo-administrador-de-prueba@example.com",
	"Password": "CONTRASENA_SEGURA_DE_PRUEBA"
  }
}
```

Para Gmail, activa la verificación en dos pasos y usa una contraseña de aplicación. Si una contraseña SMTP se expuso accidentalmente, revócala y genera otra. `AdminSeed` es opcional: si se configura, al iniciar se crea esa cuenta como Administrador cuando no existe. No uses una cuenta personal de producción para pruebas.

## Ejecutar

1. Abre `MotorCare.slnx`.
2. Establece `MotorCare.Web` como proyecto de inicio.
3. Configura la cadena `DefaultConnection` y los secretos locales necesarios.
4. Restaura paquetes y ejecuta el perfil HTTPS.
5. En `/Cuenta/Registro`, crea una cuenta de prueba. Todo registro público recibe únicamente el rol `Cliente`.

El perfil HTTPS local usa `https://localhost:7023`. Los enlaces de recuperación a `localhost` solo funcionan en el equipo donde se ejecuta la aplicación; para despliegue público se debe configurar una URL pública HTTPS y un proveedor SMTP autorizado.

## Cuentas ficticias para pruebas

Estas identidades son una guía para pruebas locales; no se precargan todas automáticamente. Usa contraseñas distintas que cumplan la política configurada y guárdalas únicamente en Secretos de usuario o en un gestor local. No incluyas contraseñas reales en la entrega.

| Uso | Correo ficticio | Rol | Preparación |
|---|---|---|---|
| Administración y permisos | `admin.prueba@motorcare.test` | Administrador | Configúralo en `AdminSeed:Email` y `AdminSeed:Password` en Secretos de usuario. La aplicación crea la cuenta y asigna el rol al iniciar si no existe. |
| Flujo normal y perfil | `cliente.prueba@motorcare.test` | Cliente | Regístralo desde `/Cuenta/Registro`; el registro público asigna Cliente. |
| Bloqueo | `cliente.bloqueo@motorcare.test` | Cliente | Regístralo aparte y úsalo solo para verificar intentos fallidos y el bloqueo temporal. |
| Recuperación | `cliente.recuperacion@motorcare.test` | Cliente | Para recibir correo real, sustituye el dominio reservado `.test` por un buzón de prueba al que tengas acceso antes de registrar la cuenta. |

Las direcciones `motorcare.test` son identificadores ficticios y no reciben correo. La cuenta de recuperación debe usar un buzón controlado por el estudiante; no publiques esa dirección si es personal. Crea estas cuentas únicamente en una base local de pruebas.

## Funciones implementadas

- Registro con correo único y validación de contraseña mediante Identity; mensajes de validación localizados al español.
- Inicio de sesión con opción «Recordarme» y cierre de sesión por POST antiforgery.
- Bloqueo temporal tras 3 intentos fallidos durante 15 minutos; el inicio de sesión mantiene un mensaje genérico, incluso durante el bloqueo.
- Cookies HttpOnly, Secure y SameSite=Lax; duración configurada de 30 minutos con renovación deslizante.
- Perfil propio y cambio de contraseña con verificación de la contraseña actual.
- Administración exclusiva para Administrador: listado de usuarios, cambio entre roles Cliente/Administrador y habilitación/deshabilitación.
- Protección contra quitar el rol o deshabilitar al último Administrador habilitado.
- Recuperación de contraseña por correo SMTP real; los tokens de Identity vencen en dos horas y son de un solo uso.

## Pruebas y entrega académica

La compilación de `MotorCare.Web.csproj` pasó en Visual Studio. La matriz de los casos funcionales del documento, con resultados y límites de las pruebas manuales realizadas, está en [`Evidencias-Pruebas-AA01.md`](Evidencias-Pruebas-AA01.md). Todavía quedan casos pendientes: no presentes la matriz como completamente aprobada.

El documento de la actividad solicita además un repositorio GitHub nuevo y exclusivo para AA01, el código y migraciones, este README, el script SQL, usuarios ficticios de prueba y un PDF que registre uso de IA/prompts y fuentes. El nombre según el formato del documento para este proyecto es `WEB-3AN-B1-AA01-AutenticacionSegura-LasluisaEdison`. Antes de publicar, revisa que no se incluyan Secretos de usuario, contraseñas, tokens de recuperación ni datos personales.
