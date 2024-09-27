using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Domains;

public class DomainVerificationStatus
{
    [JsonPropertyName("dkim")]
    public bool Dkim { get; set; }

    [JsonPropertyName("spf")]
    public bool Spf { get; set; }

    [JsonPropertyName("mx")]
    public bool Mx { get; set; }

    [JsonPropertyName("tracking")]
    public bool Tracking { get; set; }

    [JsonPropertyName("cname")]
    public bool Cname { get; set; }

    [JsonPropertyName("rp_cname")]
    public bool RpCname { get; set; }
}
