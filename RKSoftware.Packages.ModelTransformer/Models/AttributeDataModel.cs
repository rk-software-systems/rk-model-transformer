using System.Collections.Frozen;
using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Generations;

namespace RKSoftware.Packages.ModelTransformer.Models;

internal sealed class AttributeDataModel
{
    #region fields

    private readonly AttributeData _attr;
    private readonly ITypeSymbol _source;
    private readonly FrozenDictionary<string, IPropertySymbol> _sourceProperties;
    private readonly ITypeSymbol _target;
    private readonly FrozenDictionary<string, IPropertySymbol> _targetProperties;
    private readonly FrozenDictionary<string, IParameterSymbol> _targetConstructorParams;
    private readonly FrozenSet<string> _ignoredProperties;
    private readonly FrozenSet<string> _incorrectIgnoredProperties;
    private readonly FrozenSet<string> _notIgnoredReadonlyProperties;
    #endregion

    #region props

    public AttributeData Attribute => _attr;

    public ITypeSymbol Source => _source;

    public FrozenDictionary<string, IPropertySymbol> SourceProperties => _sourceProperties;

    public ITypeSymbol Target => _target;   

    public FrozenDictionary<string, IPropertySymbol> TargetProperties => _targetProperties;

    public FrozenDictionary<string, IParameterSymbol> TargetConstructorParams => _targetConstructorParams;

    public FrozenSet<string> IgnoredProperties => _ignoredProperties;

    public FrozenSet<string> IncorrectIgnoredProperties => _incorrectIgnoredProperties;

    public FrozenSet<string> NotIgnoredReadonlyProperties => _notIgnoredReadonlyProperties;

    #endregion

    #region ctor

    public AttributeDataModel(AttributeData attr)
    {
        _attr = attr ?? throw new ArgumentNullException(nameof(attr));

        _source = _attr.AttributeClass!.TypeArguments.First();
        _sourceProperties = GetProperties(_source);
        _target = _attr.AttributeClass!.TypeArguments.Last();
        _targetProperties = GetProperties(_target);
        _targetConstructorParams = _target.GetMembers()
            .OfType<IMethodSymbol>()
            .Where(x => x.MethodKind == MethodKind.Constructor && x.DeclaredAccessibility == Accessibility.Public)
            .Select(x => x.Parameters)
            .OrderBy(x => x.Length)
            .First()
            .ToFrozenDictionary(x => x.Name, y => y, StringComparer.Ordinal);

        var (correct, incorrect) = GetIgnoredProperties(_attr, _target);
        _ignoredProperties = correct.ToFrozenSet(StringComparer.Ordinal);
        _incorrectIgnoredProperties = incorrect.ToFrozenSet(StringComparer.Ordinal);
        _notIgnoredReadonlyProperties = GetNotIgnoredReadonlyProperties(_target, _ignoredProperties);
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


    #endregion
}
