# Evidencia de pruebas funcionales — AA01

**Proyecto:** MotorCare

**Fecha de pruebas iniciales:** 21 de noviembre de 2026

**Actualización:** 24 de noviembre de 2026

**Entorno inicial:** aplicación local HTTPS (`https://localhost:7023`), SQL Server `edison`, base `MotorCareAuthDb`.

**Método:** pruebas manuales iniciales con cuentas ficticias y resultados adicionales reportados por el estudiante.

> Se distinguen las pruebas observadas directamente de las reportadas por el estudiante. No se incluyen credenciales ni enlaces de recuperación.

## Resultados

| N.º | Caso indicado por la actividad | Resultado | Evidencia / observación |
|---:|---|---|---|
| 1 | Registro válido de Cliente | **Aprobado** | Se registró una cuenta ficticia nueva y la aplicación inició sesión y redirigió a Inicio. Una consulta SQL de solo lectura confirmó que ambas cuentas ficticias tienen el rol Cliente almacenado. |
| 2 | Correo duplicado o contraseña inválida | **Aprobado** | El correo duplicado se rechazó y la contraseña débil no creó una cuenta. Los errores se muestran ahora en español. |
| 3 | Inicio de sesión válido y logout | **Aprobado** | Inicio correcto; cierre redirigió a Inicio de sesión. Sin sesión, `/Perfil` redirigió al formulario de acceso. El perfil permitió guardar un cambio de nombre ficticio y mostró el dato actualizado. |
| 4 | Contraseña incorrecta repetida y bloqueo | **Aprobado** | El tercer intento fallido activa el bloqueo. Se repitió después del ajuste: tanto el intento fallido que activa el bloqueo como el intento con la contraseña correcta mientras seguía bloqueada mostraron exactamente «Correo o contraseña incorrectos». La expiración automática y el acceso posterior ya se habían comprobado antes. |
| 5 | Solicitud de recuperación por correo real | **Aprobado, reportado por el estudiante** | El estudiante informó el 24 de noviembre que probó correctamente la recuperación por correo. En las pruebas iniciales se observó la respuesta genérica para una dirección inexistente. |
| 6 | Restablecimiento con token válido | **Aprobado, reportado por el estudiante** | El estudiante informó que la recuperación por correo funcionó. No se observó directamente su prueba ni se solicitaron credenciales o enlaces. |
| 7 | Token ausente, inválido, vencido o reutilizado | **Aprobado, parcialmente reportado por el estudiante** | Un enlace sin token y un token inventado mostraron «Enlace no válido o vencido» sin cambiar la contraseña. El estudiante confirmó el rechazo de enlaces reutilizados y, el 24 de noviembre, también de un enlace vencido; esos dos casos no se observaron directamente. |
| 8 | Cliente intenta abrir URL administrativa | **Aprobado** | `/Administracion/Usuarios` mostró «Acceso denegado». |
| 9 | Administrador modifica rol o estado de una cuenta | **Aprobado, reportado por el estudiante** | El estudiante informó que las pruebas de administración solicitadas funcionaron. La cuenta de Administrador indicada también se había comprobado habilitada en la base local; la prueba en la interfaz no se observó directamente. |
| 10 | Cliente intenta asignarse privilegios de administrador | **Aprobado, reportado por el estudiante** | El estudiante confirmó el 24 de noviembre que probó que un Cliente no puede asignarse el rol Administrador. El intento no se observó directamente; anteriormente se observó acceso denegado a la URL administrativa. |
| 11 | Cambio de contraseña con clave actual errónea | **Aprobado** | El cambio fue rechazado y luego del ajuste de idioma el mensaje fue «La contraseña actual es incorrecta». Se verificó que aparezca una sola vez en el resumen del formulario y también se confirmó un cambio válido en la cuenta ficticia. |
| 12 | Actualizar y abrir otra pestaña | **Aprobado, reportado por el estudiante** | El estudiante confirmó que la sesión se comparte en la pestaña normal y no se extiende a incógnito. |
| 13 | Ventana de incógnito aislada | **Aprobado, reportado por el estudiante** | El estudiante confirmó que incógnito no comparte la sesión de navegación normal. |
| 14 | «Recordarme», persistencia y cierre de sesión | **Aprobado, reportado por el estudiante** | El estudiante informó que la prueba de «Recordarme» solicitada funcionó; no se observó directamente el reinicio del navegador. Antes se había observado que el logout invalidaba el acceso actual. |

## Cuentas ficticias creadas en la base local

No se incluyen contraseñas en esta evidencia.

- `cliente.aa01.20261121@motorcare.test`: registro, inicio/logout, perfil, cambio de contraseña y control de acceso.
- `cliente.bloqueo.20261121@motorcare.test`: las pruebas confirmaron tres intentos, expiración automática tras 15 minutos y rechazo con mensaje genérico durante el bloqueo. La repetición final del caso volvió a dejar la cuenta temporalmente bloqueada.

Los dominios reservados `.test` no reciben correo. Estas cuentas se crearon durante las pruebas iniciales; no se verificó si permanecen en la base después de las pruebas posteriores reportadas por el estudiante.

## Observaciones de la ejecución

- La compilación de `MotorCare.Web.csproj` terminó correctamente antes y después de localizar los errores de Identity.
- Una consulta SQL de solo lectura, limitada a las dos cuentas ficticias, confirmó que ambas tienen asignado el rol `Cliente`; otra consulta confirmó que la cuenta de Administrador indicada por el estudiante está habilitada y tiene ese rol. No se consultaron hashes de contraseña ni se modificaron datos.
- Se añadieron acciones administrativas protegidas para crear usuarios con rol Cliente o Administrador, asignar una nueva contraseña a un usuario Cliente `@motorcare.test` y eliminar únicamente usuarios Cliente de ese dominio. La compilación pasó; el 24 de noviembre el estudiante reportó que las pruebas de administración funcionaron, sin observación directa de esta sesión.
- La primera ejecución reveló mensajes de Identity en inglés para correo duplicado, contraseña débil y contraseña actual incorrecta. Se agregó `SpanishIdentityErrorDescriber`; las repeticiones de esas validaciones mostraron mensajes en español.
- El error repetido de consola se identificó en `/_vs/browserLink`: Chromium informa que la política de permisos bloquea `unload` en Browser Link de Visual Studio. No es una excepción de la aplicación; además Chromium emitió una advertencia de autofill para el formulario de contraseña. No se modificó código de la aplicación por esos avisos del entorno.
- El primer intento del formulario de cambio de contraseña mostró dos resúmenes para un mismo error; se asoció el error al campo de contraseña actual. La compilación y una prueba posterior confirmaron que ahora aparece una sola vez en el resumen de ese formulario.
- El bloqueo expiró automáticamente y la cuenta ficticia pudo iniciar sesión con su contraseña correcta tras el intervalo configurado. Se cerró esa sesión al finalizar la comprobación.
- Durante las pruebas iniciales no se ejecutó el script SQL, no se cambió la configuración SMTP ni se inició sesión con credenciales de Administrador. Las pruebas posteriores fueron realizadas y reportadas por el estudiante.

## Estado de la matriz

Los 14 casos tienen resultado aprobado, según las pruebas observadas y las confirmaciones del estudiante indicadas en la tabla. Los resultados reportados por el estudiante no fueron observados directamente; no sustituyen las evidencias adicionales que solicite el docente.
