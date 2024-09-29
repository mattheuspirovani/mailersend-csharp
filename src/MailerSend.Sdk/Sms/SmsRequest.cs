using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Sms;

public class SmsRequest
{
    [JsonPropertyName("from")]
    public string From { get; set; }

    [JsonPropertyName("to")]
    public List<string> To { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("personalization")]
    public List<Personalization>? Personalization { get; set; }

    public SmsRequest(string from, List<string> to, string text, List<Personalization>? personalization = null)
    {
        From = from;
        To = to;
        Text = text;
        Personalization = personalization;
    }
}

public class Personalization
{
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("data")]
    public Dictionary<string, string> Data { get; set; }

    public Personalization(string phoneNumber, Dictionary<string, string> data)
    {
        PhoneNumber = phoneNumber;
        Data = data;
    }
}