using System.Net;

namespace MailerSend.Sdk.Exceptions;

public class MailerSendException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string? ApiMessage { get; }
    public Dictionary<string, List<string>>? Errors { get; }

    public MailerSendException(string message, HttpStatusCode statusCode, string? apiMessage = null, Dictionary<string, List<string>>? errors = null)
        : base(message)
    {
        StatusCode = statusCode;
        ApiMessage = apiMessage;
        Errors = errors;
    }
}
