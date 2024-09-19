using MailerSend.Sdk.Activities;

namespace MailerSend.Sdk;

public class MailerSend
{
    //public IEmailClient Email { get; }
    public IActivityClient Activity { get; }

    public MailerSend(string apiKey, HttpClient? httpClient = null)
    {
        Activity = new ActivityClient(apiKey, httpClient);
    }
}