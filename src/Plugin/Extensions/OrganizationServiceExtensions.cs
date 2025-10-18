using System.Runtime.CompilerServices;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace StorageCleaner.Extensions;

/// <summary>Extension methods for working with IOrganizationService.</summary>
public static class OrganizationServiceExtensions
{
    // Cache the cast result per service instance, without preventing GC of the service.
    static readonly ConditionalWeakTable<IOrganizationService, Lazy<CrmServiceClient>> ClientCache = new();

    /// <summary>Gets the <see cref="CrmServiceClient"/> for this <see cref="IOrganizationService"/>.</summary>
    /// <param name="service">The IOrganizationService instance.</param>
    /// <returns>The cached <see cref="CrmServiceClient"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the underlying implementation is not a <see cref="CrmServiceClient"/>.</exception>
    public static CrmServiceClient Client(this IOrganizationService service)
    {
        if (service == null) throw new ArgumentNullException(nameof(service));

        // Lazily compute the cast once per service instance
        var lazy = ClientCache.GetValue(service, s =>
            new Lazy<CrmServiceClient>(() =>
                s as CrmServiceClient
                ?? throw new InvalidOperationException("The provided IOrganizationService cannot be cast to a CrmServiceClient."),
            isThreadSafe: true));

        return lazy.Value;
    }
}
