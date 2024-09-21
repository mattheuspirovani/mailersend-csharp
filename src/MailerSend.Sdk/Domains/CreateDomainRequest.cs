using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Domains;

public class CreateDomainRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("return_path_subdomain")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public string? ReturnPathSubdomain { get; set; }

    [JsonPropertyName("custom_tracking_subdomain")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public string? CustomTrackingSubdomain { get; set; }

    [JsonPropertyName("inbound_routing_subdomain")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] 
    public string? InboundRoutingSubdomain { get; set; }

    public CreateDomainRequest(string name)
    {
        Name = name;
    }
}
