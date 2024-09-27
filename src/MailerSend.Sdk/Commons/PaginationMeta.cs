using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Commons;

public class PaginationMeta
{
    [JsonPropertyName("current_page")]
    public int? CurrentPage { get; set; }

    [JsonPropertyName("from")]
    public int? From { get; set; }

    [JsonPropertyName("last_page")]
    public int? LastPage { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; } = null!;

    [JsonPropertyName("per_page")]
    public int? PerPage { get; set; }

    [JsonPropertyName("to")]
    public int? To { get; set; }

    [JsonPropertyName("total")]
    public int? Total { get; set; }
}
