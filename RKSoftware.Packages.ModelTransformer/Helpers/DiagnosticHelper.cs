using Microsoft.CodeAnalysis;

namespace RKSoftware.Packages.ModelTransformer.Helpers;

internal class DiagnosticHelper
{
    #region constants

    private const string _idProfix = "RKMT";

    #endregion

    public static void CreateInvalidIgnoredPropertyNameWarning(SourceProductionContext context, ITypeSymbol targetType, IEnumerable<string> props)
    {
        var descriptor = new DiagnosticDescriptor(
                        id: $"{_idProfix}001",
                        title: "Invalid property name",
                        messageFormat: "Input parameters are not property names of '{0}': {1}",
                        category: "Usage",
                        DiagnosticSeverity.Warning,
                        isEnabledByDefault: true);
        var diagnostic = Diagnostic.Create(descriptor, Location.None, targetType.ToDisplayString(), string.Join(", ", props));
        context.ReportDiagnostic(diagnostic);
    }
}
