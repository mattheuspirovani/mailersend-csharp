using System;

namespace MailerSend.Sdk.Domains;

public interface IDomainsClient
    {
        Task<DomainListResponse?> GetDomainsAsync(DomainsListOptions? options = null);
        Task<Domain?> GetDomainAsync(string domainId);
        Task<Domain?> CreateDomainAsync(CreateDomainRequest request);
    }
