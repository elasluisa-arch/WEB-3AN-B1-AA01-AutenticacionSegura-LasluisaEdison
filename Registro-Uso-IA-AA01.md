# Registro de uso de IA, prompts y fuentes — AA01

**Asignatura:** Programación Web I  
**Actividad:** AA01 — Módulo integral de autenticación y seguridad  
**Estudiante:** [Completar nombre y apellido]  
**Proyecto:** MotorCare — Ensigma Taller Mecánico  
**Fecha de elaboración de este registro:** 21 de noviembre de 2026

> Este registro resume la asistencia recibida durante el desarrollo. Los prompts de ejemplo se presentan como paráfrasis representativas, no como una transcripción literal completa. El estudiante debe revisar el documento y completar sus datos antes de entregarlo.

## Herramientas utilizadas

- **GitHub Copilot integrado en Visual Studio:** apoyo para inspeccionar y modificar código, explicar errores, proponer implementaciones y preparar documentación.
- **Visual Studio:** edición, solución y compilaciones del proyecto.
- **PowerShell local:** comprobaciones de archivos y disponibilidad de herramientas.
- **Documentación oficial de Microsoft Learn:** consulta de Identity, almacenamiento local de secretos, cookies de autenticación y migraciones de EF Core.
- **Documento de la actividad (`WEB_AA01_AutenticacionSegura.pdf`):** fuente de los requisitos funcionales, casos mínimos y entregables.

La IA se utilizó como apoyo; el estudiante revisó cambios y reportó pruebas manuales. No se incluyen credenciales, tokens ni datos de cuentas reales.

## Finalidad y componentes apoyados

La asistencia se enfocó en implementar y documentar el módulo solicitado sin rehacer la página institucional ni el CRUD de mantenimientos:

- **Presentación:** vistas MVC de registro, inicio/cierre de sesión, recuperación de contraseña, perfil, acceso denegado y administración de usuarios.
- **Negocio e identidad:** registro público como Cliente, creación administrativa de cuentas Cliente/Administrador, autenticación, bloqueo temporal, cambio y recuperación de contraseña, roles y autorización.
- **Administración:** editar roles/estado, asignar una contraseña nueva a cuentas ficticias Cliente `@motorcare.test` y eliminar solo cuentas Cliente de ese dominio; no se consultan contraseñas actuales.
- **Datos e infraestructura:** ASP.NET Core Identity con EF Core/SQL Server, migraciones y servicio de correo SMTP.
- **Seguridad:** cookies de autenticación, protección antiforgery, protección de rutas, secretos locales y protección del último administrador habilitado.
- **Entrega:** README e instrucciones SQL, además de este registro.

## Prompts representativos

Los siguientes ejemplos resumen solicitudes realizadas durante la conversación; no deben interpretarse como citas textuales exactas:

1. «Revisa el código del proyecto y compáralo con los requisitos del documento de la actividad; identifica lo que falta».
2. «Continúa el módulo de autenticación segura: registro, inicio de sesión, roles, recuperación mediante correo real y control de acceso».
3. «Al bloquear la cuenta tras intentos fallidos, muestra al usuario el tiempo aproximado restante y aplica el límite solicitado de tres intentos».
4. «Añade estilos y un logo antes de cerrar el proyecto».
5. «Prepara los entregables e instrucciones, pero deja las pruebas restantes para después de terminar el proyecto».
6. «Coteja el proyecto con el PDF de la actividad e indica qué entregables faltan».
7. «Agrega al panel de administración la creación de usuarios con rol Cliente o Administrador y el manejo de cuentas ficticias».

## Correcciones y decisiones realizadas

- El umbral de bloqueo se configuró en **3 intentos fallidos** y la duración en **15 minutos**. Aunque inicialmente se mostró el tiempo restante a petición del estudiante, luego se priorizó el requisito de mensajes genéricos del documento. La compilación y una repetición manual confirmaron que tanto el intento que activa el bloqueo como una contraseña correcta durante el bloqueo muestran el mismo mensaje genérico.
- La recuperación se implementó con tokens de Identity y envío SMTP real, no con un enlace simulado en consola. La vigencia configurada del token es de dos horas.
- La configuración SMTP y la cuenta inicial opcional de administrador se documentaron para **Secretos de usuario**; no se deben publicar contraseñas en archivos de configuración ni en GitHub.
- Se prepararon un README y un script SQL idempotente generado desde las migraciones existentes.
- Las pruebas manuales detectaron mensajes de Identity en inglés. Se agregó `SpanishIdentityErrorDescriber` y se verificó en la interfaz que las validaciones de contraseña, duplicados y contraseña actual aparezcan en español.
- Se añadió al panel Administrar usuarios la creación de cuentas con rol Cliente o Administrador, además del cambio de contraseña y eliminación restringidos a cuentas ficticias Cliente `@motorcare.test`. Se compiló la aplicación; el 24 de noviembre el estudiante reportó que las pruebas de administración funcionaron.
- El repositorio privado exclusivo de AA01 se publicó en GitHub. Este repositorio cubre autenticación y administración de usuarios, pero no contiene el CRUD de mantenimientos del proyecto principal del taller.
- Los resultados observados y los casos aún pendientes se registraron en `Evidencias-Pruebas-AA01.md`; no se presenta la matriz como completamente aprobada.

## Verificaciones y límites

- La compilación de `MotorCare.Web.csproj` se ejecutó en Visual Studio y terminó correctamente después de los cambios funcionales y de localización.
- En pruebas manuales locales se confirmó registro, errores por correo duplicado y contraseña débil, inicio/cierre, bloqueo al tercer intento con error genérico, expiración automática tras 15 minutos, protección de `/Perfil` y del área administrativa para un Cliente, guardado de perfil y cambio de contraseña. El detalle por caso y sus limitaciones está en `Evidencias-Pruebas-AA01.md`.
- La recuperación devolvió el mismo resultado genérico para una dirección inexistente; un token inventado fue rechazado sin cambiar la contraseña. El estudiante había confirmado previamente el rechazo de un enlace reutilizado y el 24 de noviembre reportó que la recuperación por correo funcionó. No se observó directamente esa prueba posterior; la expiración de un token real no se confirmó específicamente.
- El estudiante reportó que la sesión normal se comparte entre pestañas y que incógnito no la hereda; el 24 de noviembre informó también que funcionaron las pruebas solicitadas de administración y «Recordarme». Esos resultados son reportados, no observados directamente. Una consulta SQL anterior confirmó los roles Cliente de las dos cuentas ficticias y el rol Administrador habilitado de la cuenta indicada.
- La cuenta ficticia de bloqueo pudo iniciar sesión después de que expirara el primer bloqueo. La repetición final del caso la dejó temporalmente bloqueada otra vez en la base local. No se modificaron credenciales de cuentas existentes conocidas.
- El error repetido de consola se identificó en `/_vs/browserLink`: Chromium informa que la política de permisos bloquea `unload` en Browser Link de Visual Studio; no es una excepción de la aplicación. Chromium también emitió una advertencia de autofill para el formulario de contraseña.
- Los resultados no representan la matriz completa de aceptación; distinguen compilaciones verificadas, pruebas observadas y pruebas informadas anteriormente por el estudiante.

## Fuentes consultadas

1. Instituto Superior Tecnológico Cordillera, **WEB_AA01_AutenticacionSegura.pdf**, actividad autónoma AA01, 4 páginas. Requisitos de arquitectura, funciones, seguridad, pruebas y entregables.
2. Código y documentación del proyecto MotorCare: `MotorCare.Web/Program.cs`, `MotorCare.Web/Controllers/CuentaController.cs`, `MotorCare.Web/Security/SpanishIdentityErrorDescriber.cs`, `MotorCare.Data/AppDbContext.cs`, migraciones de `MotorCare.Data`, `README.md`, `Evidencias-Pruebas-AA01.md` y `MotorCare.Data/MotorCareAuthDb.sql`.
3. Microsoft Learn, **Introduction to Identity on ASP.NET Core**: <https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-11.0> (consulta: 21-11-2026).
4. Microsoft Learn, **Safe storage of app secrets in development**: <https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-11.0> (consulta: 21-11-2026).
5. Microsoft Learn, **Configure ASP.NET Core Identity**: <https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-11.0> (consulta: 21-11-2026).
6. Microsoft Learn, **Migrations Overview — EF Core**: <https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/> (consulta: 21-11-2026).

## Revisión antes de entregar

- Completar el nombre del estudiante y confirmar que las paráfrasis representan el uso real de IA.
- Confirmar específicamente el rechazo de un token vencido y el intento de autoasignación del rol Administrador por un Cliente; conservar la evidencia solicitada por el docente.
- No incluir contraseñas, secretos SMTP, tokens, capturas con credenciales ni datos personales.
- El repositorio privado exclusivo de AA01 ya está publicado; entregar los elementos por el formulario institucional indicado por el docente.
