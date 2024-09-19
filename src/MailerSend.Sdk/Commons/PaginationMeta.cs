using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Commons;

public class PaginationMeta
{
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
}
