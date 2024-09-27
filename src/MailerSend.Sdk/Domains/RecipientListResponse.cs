using System.Text.Json.Serialization;
using MailerSend.Sdk.Commons;

namespace MailerSend.Sdk.Domains;

public class RecipientListResponse
{
    [JsonPropertyName("data")]
    public List<Recipient>? Data { get; set; }

    [JsonPropertyName("links")]
    public PaginationLinks? Links { get; set; }

    [JsonPropertyName("meta")]
    public PaginationMeta? Meta { get; set; }
}
