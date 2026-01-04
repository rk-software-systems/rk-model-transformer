using System.Collections.Frozen;
using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Extensions;

namespace RKSoftware.Packages.ModelTransformer.Models;

internal sealed class AttributeDataModel
{
    #region props
    public string HostNamespace { get; }

    public string Key { get; }

    public string ClassName { get; }

    public AttributeData Attribute { get; }

    public ITypeSymbol Source { get; }

    public FrozenDictionary<string, IPropertySymbol> SourceProperties { get; }

    public ITypeSymbol Target { get; }

    public FrozenDictionary<string, IPropertySymbol> TargetProperties { get; }

    public FrozenDictionary<string, IParameterSymbol> TargetConstructorParams { get; }

    public FrozenSet<string> IgnoredProperties { get; }

    public FrozenSet<string> IncorrectIgnoredProperties { get; }

    public FrozenSet<string> NotIgnoredReadonlyProperties { get; }

    public FrozenSet<string> NotNullableIgnoredProperties { get; }

    public FrozenSet<string> PropertiesWithoutDefaultMapping { get; }

    public FrozenSet<string> IncorrectPropertiesWithoutDefaultMapping { get; }

    #endregion

    #region ctor

    public AttributeDataModel(string hostNamespace, AttributeData attr)
    {
        HostNamespace = hostNamespace;
        Attribute = attr ?? throw new ArgumentNullException(nameof(attr));
        Source = attr.AttributeClass!.TypeArguments.First();
        Key = Source.OriginalDefinition.ToDisplayString();
        ClassName = $"{Source.Name}Extensions";
        SourceProperties = GetProperties(Source, true);
        Target = attr.AttributeClass!.TypeArguments.Last();
        TargetProperties = GetProperties(Target, false);
        TargetConstructorParams = Target.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(x => x.MethodKind == MethodKind.Constructor && x.DeclaredAccessibility == Accessibility.Public)
            .Select(x => x.Parameters)
            .OrderBy(x => x.Length)
            .First()
            .ToFrozenDictionary(x => x.Name, y => y, StringComparer.Ordinal);

        var (correct, incorrect) = GetIgnoredProperties(Constants.Ignored, attr, Target);
        IgnoredProperties = correct.ToFrozenSet(StringComparer.Ordinal);
        IncorrectIgnoredProperties = incorrect.ToFrozenSet(StringComparer.Ordinal);
        NotIgnoredReadonlyProperties = GetNotIgnoredReadonlyProperties(Target, IgnoredProperties);
        NotNullableIgnoredProperties = GetNotNullableIgnoredProperties(TargetProperties, IgnoredProperties);

        (correct, incorrect) = GetIgnoredProperties(Constants.WithoutDefaultMapping, attr, Target);
        PropertiesWithoutDefaultMapping = correct.ToFrozenSet(StringComparer.Ordinal);
        IncorrectPropertiesWithoutDefaultMapping = incorrect.ToFrozenSet(StringComparer.Ordinal);
    }

    #endregion

    #region helpers

    private static (HashSet<string>, HashSet<string>) GetIgnoredProperties(string featureName, AttributeData attr, ITypeSymbol target)
    {
        var namedArgument = attr.NamedArguments
            .FirstOrDefault(x => featureName.Equals(x.Key, StringComparison.Ordinal));

        var correct = new HashSet<string>();
        var incorrect = new HashSet<string>();

        if (namedArgument.Value.Kind == TypedConstantKind.Array && !namedArgument.Value.Values.IsDefaultOrEmpty)
        {
            foreach (var tc in namedArgument.Value.Values)
            {
                if (tc.Value is string v && !string.IsNullOrWhiteSpace(v))
                {
                    if (ExistsInTarget(target, v))
                    {
                        correct.Add(v);
                    }
                    else
                    {
                        incorrect.Add(v);
                    }
                }
            }
        }

        return (correct, incorrect);
    }

    private static bool ExistsInTarget(ITypeSymbol target, string prop)
    {
        var targetProps = target.GetAllProperties(true);
        return targetProps.Any(p => string.Equals(p.Name, prop, StringComparison.Ordinal));
    }

    private static FrozenSet<string> GetNotIgnoredReadonlyProperties(ITypeSymbol target, FrozenSet<string> ignoredProperties)
    {
        return target.GetAllProperties(true)
            .Where(p => p.IsReadOnly &&  !ignoredProperties.Contains(p.Name))
            .Select(p => p.Name)
            .ToFrozenSet(StringComparer.Ordinal);
    }

    private static FrozenDictionary<string, IPropertySymbol> GetProperties(ITypeSymbol type, bool includeReadOnly)
    {
        return type.GetAllProperties(includeReadOnly)
            .ToFrozenDictionary(x => x.Name, y => y, StringComparer.Ordinal);
    }

    private static FrozenSet<string> GetNotNullableIgnoredProperties(FrozenDictionary<string, IPropertySymbol> targetProperties, FrozenSet<string> ignoredProperties)
    {
        return targetProperties
            .Where(kv => !kv.Value.IsNullable() && ignoredProperties.Contains(kv.Key))
            .Select(kv => kv.Key)
            .ToFrozenSet(StringComparer.Ordinal);
    }
    #endregion
}
