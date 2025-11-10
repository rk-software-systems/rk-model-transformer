using Microsoft.CodeAnalysis;

namespace RKSoftware.Packages.ModelTransformer.Models;

internal sealed class RegistrationModel(INamedTypeSymbol host)
{
    private readonly INamedTypeSymbol _host = host ?? throw new ArgumentNullException(nameof(host));

    public INamedTypeSymbol Host => _host;

#pragma warning disable CA1002 // Do not expose generic lists
    public List<AttributeDataModel> Attributes { get; } = [];
#pragma warning restore CA1002 // Do not expose generic lists

    public string HostNamespace => Host.ContainingNamespace.ToDisplayString();
}
