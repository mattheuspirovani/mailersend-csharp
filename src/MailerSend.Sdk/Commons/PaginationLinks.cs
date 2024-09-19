using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Commons;

public class PaginationLinks
{
    [JsonPropertyName("prev")]
    public string? Prev { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }
}
