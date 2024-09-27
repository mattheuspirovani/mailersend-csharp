using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Commons;

public class PaginationLinks
{
    [JsonPropertyName("first")]
    public string First { get; set; } = null!;

    [JsonPropertyName("last")]
    public string Last { get; set; } = null!;

    [JsonPropertyName("prev")]
    public string? Prev { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }
}
