using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Domains;

public class DomainVerificationStatusResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public DomainVerificationStatus? Data { get; set; }
}
