using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Emails;

public class SendEmailRequest
{
    [JsonPropertyName("from")]
    public EmailSender? From { get; set; }

    [JsonPropertyName("to")]
    public List<EmailRecipient> To { get; set; }

    [JsonPropertyName("cc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<EmailRecipient>? Cc { get; set; }

    [JsonPropertyName("bcc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<EmailRecipient>? Bcc { get; set; }

    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("html")]
    public string? Html { get; set; }

    [JsonPropertyName("attachments")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<EmailAttachment>? Attachments { get; set; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; set; }

    [JsonPropertyName("reply_to")]
    public EmailRecipient? ReplyTo { get; set; }

    [JsonPropertyName("personalization")]
    public List<EmailPersonalization>? Personalization { get; set; }

    [JsonPropertyName("send_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? SendAt { get; set; }

    [JsonPropertyName("template_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TemplateId { get; set; }

    public SendEmailRequest(EmailSender from, List<EmailRecipient> to, string subject, string? text)
    {
        From = from;
        To = to;
        Subject = subject;
        Text = text;
    }

    public SendEmailRequest(string templateId,  List<EmailRecipient> to)
    {
        TemplateId = templateId;
        To = to;
    }

    public void Validate()
    {
        ValidateFromAndTemplateId();
        ValidateToRecipients();
        ValidateCcRecipients();
        ValidateBccRecipients();
        ValidateRecipientNames(To, "to");
        if (Cc != null) ValidateRecipientNames(Cc, "cc");
        if (Bcc != null) ValidateRecipientNames(Bcc, "bcc");
        ValidateSubject();
        ValidateContent();
        ValidateAttachments();
        ValidateTags();
        ValidatePersonalization();
        ValidateSendAt();
    }

    private void ValidateFromAndTemplateId()
    {
        if (string.IsNullOrWhiteSpace(TemplateId) && From == null)
        {
            throw new ValidationException("The 'from' field is required unless 'template_id' is present.");
        }

        if (From != null && From.Email == null)
        {
            throw new ValidationException("The 'from.email' field is required and must be a verified domain or subdomain.");
        }
    }

    private void ValidateToRecipients()
    {
        if (To == null || To.Count == 0)
        {
            throw new ValidationException("At least one 'to' recipient is required.");
        }

        if (To.Count > 50)
        {
            throw new ValidationException("You can have a maximum of 50 'to' recipients.");
        }
    }

    private void ValidateCcRecipients()
    {
        if (Cc != null && Cc.Count > 10)
        {
            throw new ValidationException("You can have a maximum of 10 'cc' recipients.");
        }
    }

    private void ValidateBccRecipients()
    {
        if (Bcc != null && Bcc.Count > 10)
        {
            throw new ValidationException("You can have a maximum of 10 'bcc' recipients.");
        }
    }

    private void ValidateRecipientNames(List<EmailRecipient> recipients, string recipientType)
    {
        foreach (var recipient in recipients)
        {
            if (!string.IsNullOrWhiteSpace(recipient.Name) && (recipient.Name.Contains(";") || recipient.Name.Contains(",")))
            {
                throw new ValidationException($"{recipientType.ToUpper()} recipient names may not contain ';' or ','.");
            }
        }
    }

    private void ValidateSubject()
    {
        if (string.IsNullOrWhiteSpace(TemplateId) && string.IsNullOrWhiteSpace(Subject))
        {
            throw new ValidationException("The 'subject' field is required unless 'template_id' is present.");
        }

        if (Subject != null && Subject.Length > 998)
        {
            throw new ValidationException("The 'subject' field can have a maximum length of 998 characters.");
        }
    }

    private void ValidateContent()
    {
        if (string.IsNullOrWhiteSpace(Text)
            && string.IsNullOrWhiteSpace(Html)
            && string.IsNullOrWhiteSpace(TemplateId))
        {
            throw new ValidationException("Either 'text', 'html', or 'template_id' must be provided.");
        }

        if (Text != null && Text.Length > 2097152)
        {
            throw new ValidationException("The 'text' field can have a maximum size of 2 MB.");
        }

        if (Html != null && Html.Length > 2097152)
        {
            throw new ValidationException("The 'html' field can have a maximum size of 2 MB.");
        }
    }

    private void ValidateAttachments()
    {
        if (Attachments != null)
        {
            foreach (var attachment in Attachments)
            {
                if (string.IsNullOrWhiteSpace(attachment.Content))
                {
                    throw new ValidationException("Each attachment must have content.");
                }

                if (string.IsNullOrWhiteSpace(attachment.Filename))
                {
                    throw new ValidationException("Each attachment must have a filename.");
                }

                if (string.IsNullOrWhiteSpace(attachment.Disposition) ||
                    (attachment.Disposition != "inline" && attachment.Disposition != "attachment"))
                {
                    throw new ValidationException("The disposition must be either 'inline' or 'attachment'.");
                }
            }
        }
    }

    private void ValidateTags()
    {
        if (Tags != null && Tags.Count > 5)
        {
            throw new ValidationException("You can have a maximum of 5 tags.");
        }
    }

    private void ValidatePersonalization()
    {
        if (Personalization != null)
        {
            foreach (var personalization in Personalization)
            {
                if (string.IsNullOrWhiteSpace(personalization.Email))
                {
                    throw new ValidationException("Each personalization must have an email.");
                }

                if (personalization.Data == null || personalization.Data.Count == 0)
                {
                    throw new ValidationException("Each personalization must contain at least one data key-value pair.");
                }
            }
        }
    }

    private void ValidateSendAt()
    {
        if (SendAt.HasValue)
        {
            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var maxTime = currentTime + (72 * 3600); // 72 hours
            if (SendAt.Value < currentTime || SendAt.Value > maxTime)
            {
                throw new ValidationException("The 'send_at' timestamp must be between now and 72 hours in the future.");
            }
        }
    }
}

public class EmailSender
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    public EmailSender(string name, string email)
    {
        Name = name;
        Email = email;
    }
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

public class EmailSettings
{
    [JsonPropertyName("track_clicks")]
    public bool TrackClicks { get; set; }
    [JsonPropertyName("track_opens")]
    public bool TrackOpens { get; set; }
    [JsonPropertyName("track_content")]
    public bool TrackContent { get; set; }
}

public enum EmailSendStatus
{
    Queued,
    Paused
}