# Evidencia de pruebas funcionales — AA01

**Proyecto:** MotorCare  
**Fecha:** 21 de noviembre de 2026  
**Entorno:** aplicación local HTTPS (`https://localhost:7023`), SQL Server `edison`, base `MotorCareAuthDb`.  
**Método:** pruebas manuales en navegador con cuentas ficticias; no se modificaron cuentas existentes conocidas.

> Los resultados se limitan a lo observado. Los casos con correo real, sesión de administrador o segundo contexto de navegador no se marcaron como completos sin contar con esas condiciones.

## Resultados

| N.º | Caso indicado por la actividad | Resultado | Evidencia / observación |
|---:|---|---|---|
| 1 | Registro válido de Cliente | **Aprobado, rol pendiente de verificación directa** | Se registró una cuenta ficticia nueva y la aplicación inició sesión y redirigió a Inicio. El flujo asigna el rol Cliente en el código; no se consultó la fila de roles en la base. |
| 2 | Correo duplicado o contraseña inválida | **Aprobado** | El correo duplicado se rechazó y la contraseña débil no creó una cuenta. Los errores se muestran ahora en español. |
| 3 | Inicio de sesión válido y logout | **Aprobado** | Inicio correcto; cierre redirigió a Inicio de sesión. Sin sesión, `/Perfil` redirigió al formulario de acceso. El perfil permitió guardar un cambio de nombre ficticio y mostró el dato actualizado. |
| 4 | Contraseña incorrecta repetida y bloqueo | **Aprobado** | El tercer intento fallido activa el bloqueo. Se repitió después del ajuste: tanto el intento fallido que activa el bloqueo como el intento con la contraseña correcta mientras seguía bloqueada mostraron exactamente «Correo o contraseña incorrectos». La expiración automática y el acceso posterior ya se habían comprobado antes. |
| 5 | Solicitud de recuperación por correo real | **Parcial** | Se probó que una dirección ficticia inexistente recibe la misma respuesta genérica de solicitud. El estudiante había confirmado previamente recepción de un correo de recuperación en Gmail, pero no se repitió el envío ni se completó el flujo porque no se proporcionó un buzón de prueba para un correo real. |
| 6 | Restablecimiento con token válido | **Pendiente** | Requiere un buzón real de prueba y completar el formulario usando el enlace recibido. |
| 7 | Token ausente, inválido, vencido o reutilizado | **Parcial** | Un enlace sin token y un token inventado para la cuenta ficticia mostraron «Enlace no válido o vencido» sin cambiar la contraseña. En una prueba anterior el estudiante confirmó el rechazo de un enlace reutilizado. No se esperó la expiración de un token real. |
| 8 | Cliente intenta abrir URL administrativa | **Aprobado** | `/Administracion/Usuarios` mostró «Acceso denegado». |
| 9 | Administrador modifica rol o estado de una cuenta | **Pendiente** | No se inició sesión como administrador ni se alteraron los permisos de cuentas existentes. |
| 10 | Cliente intenta asignarse privilegios de administrador | **Pendiente** | No hay un flujo público de autoasignación; el intento funcional requiere verificar acciones administrativas con un contexto autorizado. |
| 11 | Cambio de contraseña con clave actual errónea | **Aprobado** | El cambio fue rechazado y luego del ajuste de idioma el mensaje fue «La contraseña actual es incorrecta». Se verificó que aparezca una sola vez en el resumen del formulario y también se confirmó un cambio válido en la cuenta ficticia. |
| 12 | Actualizar y abrir otra pestaña | **Aprobado, reportado por el estudiante** | El estudiante confirmó que la sesión se comparte en la pestaña normal y no se extiende a incógnito. |
| 13 | Ventana de incógnito aislada | **Aprobado, reportado por el estudiante** | El estudiante confirmó que incógnito no comparte la sesión de navegación normal. |
| 14 | «Recordarme», persistencia y cierre de sesión | **Parcial** | Se marcó «Recordarme» e inició sesión; el logout invalidó el acceso actual. No se cerró y reabrió el navegador para medir la persistencia de la cookie. |

## Cuentas ficticias creadas en la base local

No se incluyen contraseñas en esta evidencia.

- `cliente.aa01.20261121@motorcare.test`: registro, inicio/logout, perfil, cambio de contraseña y control de acceso.
- `cliente.bloqueo.20261121@motorcare.test`: las pruebas confirmaron tres intentos, expiración automática tras 15 minutos y rechazo con mensaje genérico durante el bloqueo. La repetición final del caso volvió a dejar la cuenta temporalmente bloqueada.

Los dominios reservados `.test` no reciben correo. La aplicación se conectó a la base local configurada por el proyecto; las cuentas ficticias creadas permanecen allí para revisión y deben eliminarse solo cuando el estudiante decida que ya no las necesita.

## Observaciones de la ejecución

- La compilación de `MotorCare.Web.csproj` terminó correctamente antes y después de localizar los errores de Identity.
- La primera ejecución reveló mensajes de Identity en inglés para correo duplicado, contraseña débil y contraseña actual incorrecta. Se agregó `SpanishIdentityErrorDescriber`; las repeticiones de esas validaciones mostraron mensajes en español.
- El error repetido de consola se identificó en `/_vs/browserLink`: Chromium informa que la política de permisos bloquea `unload` en Browser Link de Visual Studio. No es una excepción de la aplicación; además Chromium emitió una advertencia de autofill para el formulario de contraseña. No se modificó código de la aplicación por esos avisos del entorno.
- El primer intento del formulario de cambio de contraseña mostró dos resúmenes para un mismo error; se asoció el error al campo de contraseña actual. La compilación y una prueba posterior confirmaron que ahora aparece una sola vez en el resumen de ese formulario.
- El bloqueo expiró automáticamente y la cuenta ficticia pudo iniciar sesión con su contraseña correcta tras el intervalo configurado. Se cerró esa sesión al finalizar la comprobación.
- No se ejecutó el script SQL, no se cambió la configuración SMTP y no se probaron credenciales de administrador.

## Pendientes para cerrar la matriz

1. El estudiante eligió no enviar un correo real; queda pendiente completar el restablecimiento con un entorno SMTP de pruebas controlado o con un buzón autorizado.
2. Probar administración de roles/estado con una sesión autorizada de administrador. El estudiante dice haber iniciado sesión, pero el navegador de pruebas accesible desde Visual Studio sigue en la pantalla de login; verificar en el mismo contexto antes de editar el usuario ficticio y no modificar al último administrador.
3. Cerrar y volver a abrir el navegador para completar la persistencia de «Recordarme».
4. Consultar el rol almacenado de la cuenta registrada.
