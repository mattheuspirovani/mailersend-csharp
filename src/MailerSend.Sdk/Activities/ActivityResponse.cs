using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Activities;

public class ActivityResponse
{
    [JsonPropertyName("data")]
    public Activity? Data { get; set; }
}
