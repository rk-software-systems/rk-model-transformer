using Microsoft.CodeAnalysis;

namespace RKSoftware.Packages.ModelTransformer;

internal sealed class ModelTransformerRegistrationModel(INamedTypeSymbol host)
{
    private readonly INamedTypeSymbol _host = host ?? throw new ArgumentNullException(nameof(host));

    public INamedTypeSymbol Host
    {
        get => _host;
    }

#pragma warning disable CA1002 // Do not expose generic lists
    public List<INamedTypeSymbol> Attributes { get; } = [];
#pragma warning restore CA1002 // Do not expose generic lists

    public string Namespace
    {
        get
        {
            return Host.ContainingNamespace.ToDisplayString();
        }
    }
}
