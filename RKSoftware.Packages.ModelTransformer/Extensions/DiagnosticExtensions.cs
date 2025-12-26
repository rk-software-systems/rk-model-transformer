using System.Runtime.InteropServices.ComTypes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace RKSoftware.Packages.ModelTransformer.Extensions;

internal static class DiagnosticExtensions
{
    #region constants

    private const string _idProfix = "RKMT";

    #endregion

    public static void CreateInvalidPropertyNameWarning(
        this SourceProductionContext context,
        Location location,
        ITypeSymbol sourceType,
        ITypeSymbol targetType, 
        IEnumerable<string> propertyNames)
    {
        var descriptor = new DiagnosticDescriptor(
                        id: $"{_idProfix}001",
                        title: "Invalid property name",
                        messageFormat: $"'{Constants.IgnoredProperties}' contains invalid property names of '{{1}}' in <{{0}}, {{1}}>: {{2}}",
                        category: "Usage",
                        DiagnosticSeverity.Warning,
                        isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, location, sourceType.Name, targetType.Name, string.Join(", ", propertyNames));
        context.ReportDiagnostic(diagnostic);
    }

    public static void CreateReadonlyPropertyMustBeIgnoredWarning(
        this SourceProductionContext context,
        Location location,
        ITypeSymbol sourceType,
        ITypeSymbol targetType, 
        IEnumerable<string> propertyNames)
    {
        var descriptor = new DiagnosticDescriptor(
                        id: $"{_idProfix}002",
                        title: "Readonly property should be ignored",
                        messageFormat: $"Readonly properties from '{{1}}' must be added to '{Constants.IgnoredProperties}' in <{{0}}, {{1}}>: {{2}}",
                        category: "Usage",
                        DiagnosticSeverity.Warning,
                        isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, location, sourceType.Name, targetType.Name, string.Join(", ", propertyNames));
        context.ReportDiagnostic(diagnostic);
    }

    public static void CreateNotNullablePropertyCanNotBeIgnoredWarning(
        this SourceProductionContext context,
        Location location,
        ITypeSymbol sourceType, 
        ITypeSymbol targetType,
        IEnumerable<string> propertyNames)
    {
        var descriptor = new DiagnosticDescriptor(
                        id: $"{_idProfix}003",
                        title: "Not nullable property should not be ignored",
                        messageFormat: $"Not nullable properties from '{{1}}' should be removed from '{Constants.IgnoredProperties}' in <{{0}}, {{1}}>: {{2}}",
                        category: "Usage",
                        DiagnosticSeverity.Warning,
                        isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, location, sourceType.Name, targetType.Name, string.Join(", ", propertyNames));
        context.ReportDiagnostic(diagnostic);
    }
}
