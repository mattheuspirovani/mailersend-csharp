using System.Net;

namespace MailerSend.Sdk.Exceptions;

public class MailerSendException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public MailerSendException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
