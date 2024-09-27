using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Domains;

public class DnsRecordsResponse
{
    [JsonPropertyName("data")]
    public DnsRecord? Data { get; set; }
}

public class DnsRecordDetails
{
    [JsonPropertyName("hostname")]
    public string? Hostname { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("value")]
    public string? Value { get; set; }

    [JsonPropertyName("priority")]
    public string? Priority { get; set; } // Este campo é opcional, presente apenas em registros MX
}