using System.Collections.Frozen;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using RKSoftware.Packages.ModelTransformer.Generations;

namespace RKSoftware.Packages.ModelTransformer.Models;

internal sealed class AttributeDataModel
{
    #region constants

    private static readonly Regex _methodNameRegex = new(@"^[A-Za-z_][A-Za-z0-9_]*$");

    #endregion


    #region fields

    private readonly AttributeData _attr;
    private readonly FrozenSet<string> _ignoredProperties;
    private readonly FrozenSet<string> _incorrectIgnoredProperties;
    private readonly string _methodName;
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

    public string MethodName
    {
        get => _methodName;
    }

    #endregion

    #region ctor

    public AttributeDataModel(AttributeData attr)
    {
        _attr = attr ?? throw new ArgumentNullException(nameof(attr));
        var (correct, incorrect) = GetIgnoredProperties();
        _ignoredProperties = correct.ToFrozenSet(StringComparer.Ordinal);
        _incorrectIgnoredProperties = incorrect.ToFrozenSet(StringComparer.Ordinal);
        _methodName = GetMethodName();
    }

    #endregion

    #region methods

    public string? GetMethodNameIfInvalid()
    {        
        return _methodNameRegex.IsMatch(_methodName) ? null : _methodName;
    }

    #endregion

    #region helpers

    private string GetMethodName()
    {
        var namedArgument = _attr.NamedArguments
            .FirstOrDefault(x => RegistrationAttributeGeneration.MethodNamePropertyName.Equals(x.Key, StringComparison.Ordinal));

        if (namedArgument.Value.Value is string methodName && !string.IsNullOrWhiteSpace(methodName))
        {
            return methodName;
        }

        return RegistrationAttributeGeneration.DefaultMethodName;
    }

    private (HashSet<string>, HashSet<string>) GetIgnoredProperties()
    {
        var correct = new HashSet<string>();
        var incorrect = new HashSet<string>();

        var namedArgument = _attr.NamedArguments
            .FirstOrDefault(x => RegistrationAttributeGeneration.IgnoredPropertiesPropertyName.Equals(x.Key, StringComparison.Ordinal));

        if (namedArgument.Value.Kind == TypedConstantKind.Array && !namedArgument.Value.Values.IsDefaultOrEmpty)
        {
            foreach (var tc in namedArgument.Value.Values)
            {
                if (tc.Value is string v && !string.IsNullOrWhiteSpace(v))
                {
                    if (ExistsInTraget(v))
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

    private bool ExistsInTraget(string prop)
    {
        var targetProps = Target.GetMembers().OfType<IPropertySymbol>();
        return targetProps.Any(p => string.Equals(p.Name, prop, StringComparison.Ordinal));
    }

    #endregion
}
