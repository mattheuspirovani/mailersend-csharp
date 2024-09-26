namespace MailerSend.Sdk.Emails;

public interface IEmailClient
{
    Task<EmailSendStatus> SendEmailAsync(SendEmailRequest request);
    Task<BulkEmailResponse?> SendBulkEmailAsync(List<SendEmailRequest> request);
}
