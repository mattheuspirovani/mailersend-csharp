using System.Text.Json.Serialization;
using MailerSend.Sdk.Commons;

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