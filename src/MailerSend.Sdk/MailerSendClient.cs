using MailerSend.Sdk.Activities;

namespace MailerSend.Sdk;

public class MailerSendClient
{
    //public IEmailClient Email { get; }
    public IActivityClient Activity { get; }

    public MailerSendClient(string apiKey, HttpClient? httpClient = null)
    {
        Activity = new ActivityClient(apiKey, httpClient);
    }
}