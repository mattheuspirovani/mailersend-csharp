namespace MailerSend.Sdk.Sms;

public class SmsResponse
{
    public string MessageId { get; set; } = null!;
    public bool IsPaused { get; set; }
}
