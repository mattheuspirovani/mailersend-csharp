using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Emails;

public class GetBulkEmailResponse
{
    [JsonPropertyName("data")]
    public BulkEmailStatus? Data { get; set; }
}
