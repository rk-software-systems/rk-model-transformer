using System.Collections.Frozen;
using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Generations;

namespace RKSoftware.Packages.ModelTransformer.Models;

internal class AttributeDataModel
{
    #region fields

    private readonly AttributeData _attr;
    private readonly FrozenSet<string> _ignoredProperties;
    private readonly FrozenSet<string> _incorrectIgnoredProperties;
    #endregion

    #region props

    public AttributeData Attribute
    {
        get => _attr;
    }

    public ITypeSymbol Source
    {
        get => _attr.AttributeClass!.TypeArguments.First();
    }

    public ITypeSymbol Target
    {
        get => _attr.AttributeClass!.TypeArguments.Last();
    }

    public FrozenSet<string> IgnoredProperties
    {
        get
        {
            return _ignoredProperties;
        }
    }

    public FrozenSet<string> IncorrectIgnoredProperties
    {
        get
        {
            return _incorrectIgnoredProperties;
        }
    }

    #endregion

    #region ctor

    public AttributeDataModel(AttributeData attr)
    {
        _attr = attr ?? throw new ArgumentNullException(nameof(attr));
        var (correct, incorrect) = GetIgnoredProperties();
        _ignoredProperties = correct.ToFrozenSet(StringComparer.Ordinal);
        _incorrectIgnoredProperties = incorrect.ToFrozenSet(StringComparer.Ordinal);
    }

    #endregion

    #region methods

    #endregion

    private (HashSet<string>, HashSet<string>) GetIgnoredProperties()
    {
        var correct = new HashSet<string>();
        var incorrect = new HashSet<string>();

        // Positional constructor arguments (supports params string[] or single string)
        if (_attr.ConstructorArguments.Length > 0)
        {
            var first = _attr.ConstructorArguments[0];
            var value = GetIgnoredPropertyValue(first);
            if (value != null)
            {
                if (ExistsInTraget(value))
                {
                    correct.Add(value);
                }
                else
                {
                    incorrect.Add(value);
                }
            }
        }

        // Named arguments (e.g. <parameterName> = new[] { "A", "B" })
        foreach (var named in _attr.NamedArguments)
        {
            if (string.Equals(named.Key, RegistrationAttributeGeneration.IgnoredPropertiesParameterName, StringComparison.Ordinal))
            {
                var namedValue = named.Value;
                var value = GetIgnoredPropertyValue(namedValue);
                if (value != null && ExistsInTraget(value))
                {
                    if (ExistsInTraget(value))
                    {
                        correct.Add(value);
                    }
                    else
                    {
                        incorrect.Add(value);
                    }
                }
            }
        }

        return (correct, incorrect);
    }

    private bool ExistsInTraget(string prop)
    {
        var targetProps = Target.GetMembers().OfType<IPropertySymbol>();
        return targetProps.Any(p => string.Equals(p.Name, prop, StringComparison.Ordinal));
    }

    private static string? GetIgnoredPropertyValue(TypedConstant arg)
    {
        if (arg.Kind == TypedConstantKind.Array)
        {
            foreach (var tc in arg.Values)
            {
                if (tc.Value is string s && !string.IsNullOrWhiteSpace(s))
                {
                    return s;
                }
            }
        }
        else if (arg.Value is string s && !string.IsNullOrWhiteSpace(s))
        {
            return s;
        }

        return null;
    }
}
