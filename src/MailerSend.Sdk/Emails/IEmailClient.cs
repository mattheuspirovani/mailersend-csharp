namespace MailerSend.Sdk.Emails;

public interface IEmailClient
{
    Task<EmailSendStatus> SendEmailAsync(SendEmailRequest request);
    Task<BulkEmailResponse?> SendBulkEmailAsync(List<SendEmailRequest> request);
    Task<BulkEmailStatus?> GetBulkEmailStatusAsync(string id);
    Task<EmailVerificationResponse?> EmailVerificationAsync(EmailVerificationRequest emailVerificationRequest);
}
