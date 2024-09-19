namespace MailerSend.Sdk.Activities;

public class ActivityListOptions
{
    public string? DomainId { get; set; }
    public string? Event { get; set; }
    public string? Recipient { get; set; }
    public string? MessageId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
}
