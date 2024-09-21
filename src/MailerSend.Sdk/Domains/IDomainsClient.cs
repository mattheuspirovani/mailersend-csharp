namespace MailerSend.Sdk.Domains;

public interface IDomainsClient
{
    Task<DomainListResponse?> GetDomainsAsync(DomainsListOptions? options = null);
    Task<Domain?> GetDomainAsync(string domainId);
    Task<Domain?> CreateDomainAsync(CreateDomainRequest request);
    Task<bool> DeleteDomainAsync(string domainId);
    Task<RecipientListResponse?> GetRecipientsAsync(string domainId, RecipientListOptions? options = null);
    Task<Domain?> UpdateDomainSettingsAsync(string domainId, UpdateDomainSettingsRequest request);
    Task<DnsRecord?> GetDnsRecordsAsync(string domainId);
}
