using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Domains;

public class UpdateDomainSettingsRequest
{
    [JsonPropertyName("send_paused")]
    public bool? SendPaused { get; set; }

    [JsonPropertyName("track_clicks")]
    public bool? TrackClicks { get; set; }

    [JsonPropertyName("track_opens")]
    public bool? TrackOpens { get; set; }

    [JsonPropertyName("track_unsubscribe")]
    public bool? TrackUnsubscribe { get; set; }

    [JsonPropertyName("track_content")]
    public bool? TrackContent { get; set; }

    [JsonPropertyName("custom_tracking_enabled")]
    public bool? CustomTrackingEnabled { get; set; }

    [JsonPropertyName("custom_tracking_subdomain")]
    public string? CustomTrackingSubdomain { get; set; }

    [JsonPropertyName("return_path_subdomain")]
    public string? ReturnPathSubdomain { get; set; }

    [JsonPropertyName("inbound_routing_enabled")]
    public bool? InboundRoutingEnabled { get; set; }

    [JsonPropertyName("inbound_routing_subdomain")]
    public string? InboundRoutingSubdomain { get; set; }

    [JsonPropertyName("precedence_bulk")]
    public bool? PrecedenceBulk { get; set; }

    [JsonPropertyName("ignore_duplicated_recipients")]
    public bool? IgnoreDuplicatedRecipients { get; set; }
}
