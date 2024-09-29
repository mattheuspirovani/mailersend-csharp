namespace MailerSend.Sdk.Sms;

public interface ISmsClient
{
    Task<SmsResponse> SendSmsAsync(SmsRequest smsRequest);
}
