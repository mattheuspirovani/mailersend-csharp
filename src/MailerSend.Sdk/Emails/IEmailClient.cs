namespace MailerSend.Sdk.Emails;

public interface IEmailClient
{
    Task SendEmailAsync(SendEmailRequest request);
}
