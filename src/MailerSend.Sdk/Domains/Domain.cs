using System;
using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Domains;

public class Domain
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("dkim")]
    public bool? Dkim { get; set; }

    [JsonPropertyName("spf")]
    public bool? Spf { get; set; }

    [JsonPropertyName("mx")]
    public bool? Mx { get; set; }

    [JsonPropertyName("tracking")]
    public bool? Tracking { get; set; }

    [JsonPropertyName("is_verified")]
    public bool IsVerified { get; set; }

    [JsonPropertyName("is_cname_verified")]
    public bool IsCnameVerified { get; set; }

    [JsonPropertyName("is_dns_active")]
    public bool IsDnsActive { get; set; }

    [JsonPropertyName("is_cname_active")]
    public bool IsCnameActive { get; set; }

    [JsonPropertyName("is_tracking_allowed")]
    public bool IsTrackingAllowed { get; set; }

    [JsonPropertyName("has_not_queued_messages")]
    public bool HasNotQueuedMessages { get; set; }

    [JsonPropertyName("not_queued_messages_count")]
    public int NotQueuedMessagesCount { get; set; }

    [JsonPropertyName("domain_settings")]
    public DomainSettings? DomainSettings { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    [JsonPropertyName("can")]
    public DomainPermissions? Can { get; set; }

    [JsonPropertyName("totals")]
    public object? Totals { get; set; } //TODO check correct format

    [JsonPropertyName("registrar")]
    public string? Registrar { get; set; }

    [JsonPropertyName("is_dkim_txt")]
    public bool? IsDkimTxt { get; set; }

    [JsonPropertyName("show_dkim_info")]
    public bool? ShowDkimInfo { get; set; }

    [JsonPropertyName("is_being_verified")]
    public bool? IsBeingVerified { get; set; }
}

public class DomainSettings
{
    [JsonPropertyName("send_paused")]
    public bool SendPaused { get; set; }

    [JsonPropertyName("track_clicks")]
    public bool TrackClicks { get; set; }

    [JsonPropertyName("track_opens")]
    public bool TrackOpens { get; set; }

    [JsonPropertyName("track_unsubscribe")]
    public bool TrackUnsubscribe { get; set; }

    [JsonPropertyName("track_unsubscribe_html")]
    public string? TrackUnsubscribeHtml { get; set; }

    [JsonPropertyName("track_unsubscribe_plain")]
    public string? TrackUnsubscribePlain { get; set; }

    [JsonPropertyName("track_content")]
    public bool TrackContent { get; set; }

    [JsonPropertyName("custom_tracking_enabled")]
    public bool CustomTrackingEnabled { get; set; }

    [JsonPropertyName("custom_tracking_subdomain")]
    public string? CustomTrackingSubdomain { get; set; }

    [JsonPropertyName("precedence_bulk")]
    public bool PrecedenceBulk { get; set; }

    [JsonPropertyName("ignore_duplicated_recipients")]
    public bool IgnoreDuplicatedRecipients { get; set; }
}

public class DomainPermissions
{
    [JsonPropertyName("manage")]
    public bool Manage { get; set; }
}