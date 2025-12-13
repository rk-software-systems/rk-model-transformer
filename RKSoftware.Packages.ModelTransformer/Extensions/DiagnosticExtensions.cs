using Microsoft.CodeAnalysis;

namespace RKSoftware.Packages.ModelTransformer.Extensions;

internal static class DiagnosticExtensions
{
    #region constants

    private const string _idProfix = "RKMT";

    #endregion

    public static void CreateInvalidPropertyNameWarning(this SourceProductionContext context, ITypeSymbol sourceType, ITypeSymbol targetType, IEnumerable<string> props)
    {
        var descriptor = new DiagnosticDescriptor(
                        id: $"{_idProfix}001",
                        title: "Invalid property name",
                        messageFormat: "IgnoredProperties contain invalid property names of '{1}' in <{0},{1}>: {2}",
                        category: "Usage",
                        DiagnosticSeverity.Warning,
                        isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, Location.None, sourceType.Name, targetType.Name, string.Join(", ", props));
        context.ReportDiagnostic(diagnostic);
    }

    public static void CreateReadonlyPropertyMustBeIgnoredWarning(this SourceProductionContext context, ITypeSymbol sourceType, ITypeSymbol targetType, IEnumerable<string> props)
    {
        var descriptor = new DiagnosticDescriptor(
                        id: $"{_idProfix}002",
                        title: "Readonly property must be ignored",
                        messageFormat: "Readonly properties from '{1}' must be added to IgnoredProperties in <{0},{1}>: {2}",
                        category: "Usage",
                        DiagnosticSeverity.Warning,
                        isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, Location.None, sourceType.Name, targetType.Name, string.Join(", ", props));
        context.ReportDiagnostic(diagnostic);
    }

    public static void CreateNotNullablePropertyCanNotBeIgnoredError(this SourceProductionContext context, ITypeSymbol sourceType, ITypeSymbol targetType, IEnumerable<string> props)
    {
        var descriptor = new DiagnosticDescriptor(
                        id: $"{_idProfix}003",
                        title: "Not nullable property can not be ignored",
                        messageFormat: "Not nullable properties from '{1}' must be removed from IgnoredProperties in <{0},{1}>: {2}",
                        category: "Usage",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, Location.None, sourceType.Name, targetType.Name, string.Join(", ", props));
        context.ReportDiagnostic(diagnostic);
    }

    public static void CreateMismatchOfPropertyTypesError(this SourceProductionContext context, ITypeSymbol sourceType, ITypeSymbol targetType)
    {
        var descriptor = new DiagnosticDescriptor(
                        id: $"{_idProfix}004",
                        title: "Mismatch of property types",
                        messageFormat: "Property type from '{0}' is not matched to property type '{1}' in <{0},{1}>.",
                        category: "Usage",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, Location.None, sourceType.Name, targetType.Name);
        context.ReportDiagnostic(diagnostic);
    }
}
