using System;
using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Emails;

public class EmailVerificationResponse
{
    [JsonPropertyName("status")]
    public EmailVerificationResult Status { get; set; }
}

public enum EmailVerificationResult
{
    [JsonPropertyName("valid")]
    Valid, // Email is safe to send.

    [JsonPropertyName("catch_all")]
    CatchAll, // Recipient's mail server will accept emails, but we cannot guarantee the address belongs to a person.

    [JsonPropertyName("mailbox_full")]
    MailboxFull, // Recipient’s inbox is full and may not be able to receive new emails.

    [JsonPropertyName("role_based")]
    RoleBased, // Email is role-based and may not be associated with a specific person.

    [JsonPropertyName("unknown")]
    Unknown, // Unable to determine if the email is valid or not valid.

    [JsonPropertyName("failed")]
    Failed, // Could not perform the check due to timeouts.

    [JsonPropertyName("syntax_error")]
    SyntaxError, // The email address is not valid due to a syntax error.

    [JsonPropertyName("typo")]
    Typo, // The email address has a typo.

    [JsonPropertyName("mailbox_not_found")]
    MailboxNotFound, // Recipient’s inbox does not exist.

    [JsonPropertyName("disposables")]
    Disposables, // The email address is a temporary (disposable) inbox.

    [JsonPropertyName("mailbox_blocked")]
    MailboxBlocked // The mailbox is blocked by its service provider due to poor sending practices.
}