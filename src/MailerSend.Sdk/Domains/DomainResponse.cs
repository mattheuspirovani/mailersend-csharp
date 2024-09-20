using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Domains;

public class DomainResponse
{
    [JsonPropertyName("data")]
    public Domain? Data { get; set; }
}
