using System;
using System.Text.Json.Serialization;
using MailerSend.Sdk.Commons;

namespace MailerSend.Sdk.Emails;

public class EmailVerificationListResponse
{
    [JsonPropertyName("data")]
    public List<EmailVerificationListItem> Data { get; set; }

    [JsonPropertyName("links")]
    public PaginationLinks Links { get; set; }

    [JsonPropertyName("meta")]
    public PaginationMeta Meta { get; set; }
}

public class EmailVerificationListItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("verification_started")]
    public DateTime? VerificationStarted { get; set; }

    [JsonPropertyName("verification_ended")]
    public DateTime? VerificationEnded { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("status")]
    public Status Status { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("statistics")]
    public Statistics Statistics { get; set; }
}

public class Status
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("count")]
    public int Count { get; set; }
}

public class Statistics
{
    [JsonPropertyName("valid")]
    public int Valid { get; set; }

    [JsonPropertyName("catch_all")]
    public int CatchAll { get; set; }

    [JsonPropertyName("mailbox_full")]
    public int MailboxFull { get; set; }

    [JsonPropertyName("role_based")]
    public int RoleBased { get; set; }

    [JsonPropertyName("unknown")]
    public int Unknown { get; set; }

    [JsonPropertyName("syntax_error")]
    public int SyntaxError { get; set; }

    [JsonPropertyName("typo")]
    public int Typo { get; set; }

    [JsonPropertyName("mailbox_not_found")]
    public int MailboxNotFound { get; set; }

    [JsonPropertyName("disposable")]
    public int Disposable { get; set; }

    [JsonPropertyName("mailbox_blocked")]
    public int MailboxBlocked { get; set; }

    [JsonPropertyName("failed")]
    public int Failed { get; set; }
}