using MailerSend.Sdk.Activities;
using MailerSend.Sdk.Domains;
using MailerSend.Sdk.Emails;
using MailerSend.Sdk.Sms;

namespace MailerSend.Sdk;

public class MailerSendClient
{
    public IActivityClient Activity { get; }
    public IDomainsClient Domains { get; }
    public IEmailClient Email { get; }

    public ISmsClient Sms { get; }

    public MailerSendClient(string apiKey, HttpClient? httpClient = null)
    {
        Activity = new ActivityClient(apiKey, httpClient);
        Domains = new DomainClient(apiKey, httpClient);
        Email = new EmailClient(apiKey, httpClient);
        Sms = new SmsClient(apiKey, httpClient);
    }

    public async Task<EmailSendStatus> SendEmailAsync(SendEmailRequest sendEmailRequest)
    {
        return await Email.SendEmailAsync(sendEmailRequest);
    }

    public async Task<BulkEmailResponse?> SendBulkEmailAsync(List<SendEmailRequest> sendEmailRequest)
    {
        return await Email.SendBulkEmailAsync(sendEmailRequest);
    }

    public async Task<SmsResponse> SendSms(SmsRequest smsRequest) 
    {
        return await Sms.SendSmsAsync(smsRequest);
    }
}