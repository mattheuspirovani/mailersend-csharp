using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Domains;

public class DnsRecord
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("spf")]
    public DnsRecordDetails? Spf { get; set; }

    [JsonPropertyName("dkim")]
    public DnsRecordDetails? Dkim { get; set; }

    [JsonPropertyName("return_path")]
    public DnsRecordDetails? ReturnPath { get; set; }

    [JsonPropertyName("custom_tracking")]
    public DnsRecordDetails? CustomTracking { get; set; }

    [JsonPropertyName("inbound_routing")]
    public DnsRecordDetails? InboundRouting { get; set; }
}
