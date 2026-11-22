namespace MotorCare.Business;

public interface IEmailService
{
    Task SendAsync
        (string recipient, 
         string subject, 
         string htmlBody, 
         string textBody, 
         CancellationToken cancellationToken = default);
}
