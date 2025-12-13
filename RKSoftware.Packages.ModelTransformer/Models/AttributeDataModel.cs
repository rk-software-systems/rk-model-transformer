using System.Collections.Frozen;
using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Extensions;
using RKSoftware.Packages.ModelTransformer.Generations;

namespace RKSoftware.Packages.ModelTransformer.Models;

internal sealed class AttributeDataModel
{
    #region props

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

    #endregion

    #region ctor

    public AttributeDataModel(AttributeData attr)
    {
        Attribute = attr ?? throw new ArgumentNullException(nameof(attr));

        Source = attr.AttributeClass!.TypeArguments.First();
        SourceProperties = GetProperties(Source);
        Target = attr.AttributeClass!.TypeArguments.Last();
        TargetProperties = GetProperties(Target);
        TargetConstructorParams = Target.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(x => x.MethodKind == MethodKind.Constructor && x.DeclaredAccessibility == Accessibility.Public)
            .Select(x => x.Parameters)
            .OrderBy(x => x.Length)
            .First()
            .ToFrozenDictionary(x => x.Name, y => y, StringComparer.Ordinal);

        var (correct, incorrect) = GetIgnoredProperties(attr, Target);
        IgnoredProperties = correct.ToFrozenSet(StringComparer.Ordinal);
        IncorrectIgnoredProperties = incorrect.ToFrozenSet(StringComparer.Ordinal);
        NotIgnoredReadonlyProperties = GetNotIgnoredReadonlyProperties(Target, IgnoredProperties);
        NotNullableIgnoredProperties = GetNotNullableIgnoredProperties(TargetProperties, IgnoredProperties);
    }

    #endregion

    #region helpers

    private static (HashSet<string>, HashSet<string>) GetIgnoredProperties(AttributeData attr, ITypeSymbol target)
    {
        var correct = new HashSet<string>();
        var incorrect = new HashSet<string>();

        var namedArgument = attr.NamedArguments
            .FirstOrDefault(x => RegistrationAttributeGeneration.IgnoredPropertiesPropertyName.Equals(x.Key, StringComparison.Ordinal));

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
        var targetProps = target.GetMembers().OfType<IPropertySymbol>();
        return targetProps.Any(p => string.Equals(p.Name, prop, StringComparison.Ordinal));
    }

    private static FrozenSet<string> GetNotIgnoredReadonlyProperties(ITypeSymbol target, FrozenSet<string> ignoredProperties)
    {
        return target.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => !p.IsStatic && 
                         p.DeclaredAccessibility == Accessibility.Public && 
                         p.IsReadOnly && 
                         !ignoredProperties.Contains(p.Name))
            .Select(p => p.Name)
            .ToFrozenSet(StringComparer.Ordinal);
    }

    private static FrozenDictionary<string, IPropertySymbol> GetProperties(ITypeSymbol type)
    {
        return type.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => !p.IsStatic &&                         
                        p.DeclaredAccessibility == Accessibility.Public &&
                        !p.IsReadOnly)
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
