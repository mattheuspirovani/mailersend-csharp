using System;
using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Exceptions;

public class ApiErrorResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("errors")]
    public Dictionary<string, List<string>>? Errors { get; set; }
}
