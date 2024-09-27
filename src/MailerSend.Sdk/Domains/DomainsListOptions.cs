using System;
using System.Text.Json.Serialization;

namespace MailerSend.Sdk.Domains;

public class DomainsListOptions
{
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public bool? Verified { get; set; }
}
