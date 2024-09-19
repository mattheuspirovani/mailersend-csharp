using MailerSend.Sdk.Activities;
using MailerSend.Sdk.Domains;

namespace MailerSend.Sdk;

public class MailerSendClient
{
    //public IEmailClient Email { get; }
    public IActivityClient Activity { get; }
    public IDomainsClient Domains { get; }

    public MailerSendClient(string apiKey, HttpClient? httpClient = null)
    {
        Activity = new ActivityClient(apiKey, httpClient);
        Domains = new DomainClient(apiKey, httpClient);
    }
}