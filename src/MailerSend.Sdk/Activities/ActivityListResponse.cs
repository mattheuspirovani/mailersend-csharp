using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Activities;

public class ActivityListResponse
{
    [JsonPropertyName("data")]
    public List<Activity>? Data { get; set; }

    [JsonPropertyName("links")]
    public PaginationLinks? Links { get; set; }

    [JsonPropertyName("meta")]
    public PaginationMeta? Meta { get; set; }
}

public class PaginationLinks
{
    [JsonPropertyName("prev")]
    public string? Prev { get; set; }

    [JsonPropertyName("next")]
    public string? Next { get; set; }
}

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
