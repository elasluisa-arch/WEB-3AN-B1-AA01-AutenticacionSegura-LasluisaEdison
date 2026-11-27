using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MotorCare.Business;

namespace MotorCare.Infrastructure;

public sealed class SmtpEmailService(IOptions<SmtpOptions> options) : IEmailService
{
    private readonly SmtpOptions _options = options.Value;

    public async Task SendAsync(
        string recipient,
        string subject,
        string htmlBody,
        string textBody,
        CancellationToken cancellationToken = default)
    {
        ValidateConfiguration();

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_options.FromName, _options.FromAddress));
        message.To.Add(MailboxAddress.Parse(recipient));
        message.Subject = subject;
        message.Body = new BodyBuilder
        {
            HtmlBody = htmlBody,
            TextBody = textBody
        }.ToMessageBody();

        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.StartTls, cancellationToken);
            await client.AuthenticateAsync(_options.UserName, _options.Password, cancellationToken);
            var result = await client.SendAsync(MimeKit.FormatOptions.Default, new MailKit.SendRequest(message), cancellationToken);
            if (result.AcceptedRecipients.Count == 0)
            {
                throw new InvalidOperationException("El servidor SMTP no aceptó al destinatario del correo.");
            }

            await client.DisconnectAsync(true, cancellationToken);
        }
        catch (Exception exception) when (exception is MailKit.ProtocolException
            or MailKit.CommandException
            or MailKit.ServiceNotConnectedException
            or MailKit.ServiceNotAuthenticatedException
            or System.Security.Authentication.AuthenticationException
            or IOException)
        {
            throw new EmailDeliveryException("No se pudo enviar el correo mediante SMTP.", exception);
        }
    }

    private void ValidateConfiguration()
    {
        if (string.IsNullOrWhiteSpace(_options.Host)
            || _options.Port is < 1 or > 65535
            || string.IsNullOrWhiteSpace(_options.UserName)
            || string.IsNullOrWhiteSpace(_options.Password)
            || string.IsNullOrWhiteSpace(_options.FromAddress))
        {
            throw new InvalidOperationException("El servicio SMTP no está configurado. Configure Smtp:Host, Smtp:Port, Smtp:UserName, Smtp:Password y Smtp:FromAddress en secretos de usuario o variables de entorno.");
        }
    }
}
