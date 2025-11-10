using Microsoft.CodeAnalysis;

namespace RKSoftware.Packages.ModelTransformer.Helpers;

internal static class DiagnosticHelper
{
    #region constants

    private const string _idProfix = "RKMT";

    #endregion

    public static void CreateInvalidPropertyNameWarning(SourceProductionContext context, ITypeSymbol sourceType, ITypeSymbol targetType, IEnumerable<string> props)
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

    public static void CreateInvalidMethodNameError(SourceProductionContext context, ITypeSymbol sourceType, ITypeSymbol targetType, string methodName)
    {
        var descriptor = new DiagnosticDescriptor(
                        id: $"{_idProfix}002",
                        title: "Invalid method name",
                        messageFormat: "MethodName '{2}' is invalid in <{0},{1}>",
                        category: "Usage",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, Location.None, sourceType.Name, targetType.Name, methodName);
        context.ReportDiagnostic(diagnostic);
    }

    public static void CreateReadonlyPropertyMustBeIgnoredWarning(SourceProductionContext context, ITypeSymbol sourceType, ITypeSymbol targetType, IEnumerable<string> props)
    {
        var descriptor = new DiagnosticDescriptor(
                        id: $"{_idProfix}003",
                        title: "Readonly property must be ignored",
                        messageFormat: "Readonly properties from '{1}' must be added to IgnoredProperties in <{0},{1}>: {2}",
                        category: "Usage",
                        DiagnosticSeverity.Warning,
                        isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, Location.None, sourceType.Name, targetType.Name, string.Join(", ", props));
        context.ReportDiagnostic(diagnostic);
    }
}
