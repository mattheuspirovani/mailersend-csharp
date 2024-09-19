namespace MailerSend.Sdk.Activities;

public class ActivityListOptions
{
    public string? DomainId { get; set; }
    public string? Event { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }

    public ActivityListOptions(DateTime dateFrom, DateTime dateTo)
    {
        if (dateFrom > dateTo)
        {
            throw new ArgumentException("The 'dateFrom' parameter cannot be later than the 'dateTo' parameter.", nameof(dateFrom));
        }
        DateFrom = dateFrom;
        DateTo = dateTo;
    }
}
