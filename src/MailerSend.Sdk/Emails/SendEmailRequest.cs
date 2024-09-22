using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Emails;

public class SendEmailRequest
{
    [JsonPropertyName("from")]
    public EmailSender From { get; set; }

    [JsonPropertyName("to")]
    public List<EmailRecipient> To { get; set; }

    [JsonPropertyName("cc")]
    public List<EmailRecipient>? Cc { get; set; }

    [JsonPropertyName("bcc")]
    public List<EmailRecipient>? Bcc { get; set; }

    [JsonPropertyName("subject")]
    public string Subject { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("html")]
    public string? Html { get; set; }

    [JsonPropertyName("attachments")]
    public List<EmailAttachment>? Attachments { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    [JsonPropertyName("reply_to")]
    public EmailRecipient? ReplyTo { get; set; }

    [JsonPropertyName("personalization")]
    public List<EmailPersonalization>? Personalization { get; set; }

    [JsonPropertyName("send_at")]
    public long? SendAt { get; set; }  // Unix timestamp for scheduling
}

public class EmailSender
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class EmailRecipient
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

public class EmailAttachment
{
    [JsonPropertyName("content")]
    public string Content { get; set; }  // Base64 encoded content

    [JsonPropertyName("filename")]
    public string Filename { get; set; }

    [JsonPropertyName("disposition")]
    public string Disposition { get; set; } = "attachment";
}

public class EmailPersonalization
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("data")]
    public Dictionary<string, string>? Data { get; set; }
}