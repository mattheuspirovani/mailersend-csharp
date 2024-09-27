using System.Text.Json.Serialization;
using MailerSend.Sdk.Commons;

namespace MailerSend.Sdk.Domains;

public class DomainListResponse
{
    [JsonPropertyName("data")]
    public List<Domain>? Data { get; set; }

    [JsonPropertyName("links")]
    public PaginationLinks? Links { get; set; }

    [JsonPropertyName("meta")]
    public PaginationMeta? Meta { get; set; }
}
