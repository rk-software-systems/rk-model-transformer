using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace RKSoftware.Packages.ModelTransformer;

/// <summary>
/// Source generator that processes classes with the [ModelTransformerRegistrationAttribute] attribute.
/// </summary>
[Generator]
public class ModelTransformerIncrementalGenerator : IIncrementalGenerator
{
    /// <summary>
    /// Initializes the incremental generator by configuring the syntax and semantic processing pipeline.
    /// </summary>
    /// <remarks>This method sets up the generator to process syntax nodes and semantic models, identify
    /// target classes  for code generation, and produce source code based on the identified targets. It registers:
    /// <list type="bullet"> <item> <description>A post-initialization action to add a marker attribute to the
    /// compilation.</description> </item> <item> <description>A syntax provider to filter and transform syntax nodes
    /// into models for code generation.</description> </item> <item> <description>A source output action to generate
    /// source code for each identified model.</description> </item> </list></remarks>
    /// <param name="context">The <see cref="IncrementalGeneratorInitializationContext"/> used to register syntax providers, 
    /// post-initialization actions, and source outputs for the generator.</param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Add the marker attribute to the compilation
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource("ModelTransformerRegistrationAttribute.g.cs", SourceText.From(ModelTransformerIncrementalGeneratorHelper.Attribute, Encoding.UTF8)));

        IncrementalValuesProvider<ModelTransformerRegistrationModel?> classesToGenerate = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // select classes with attributes
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // select classes with the [Ordered] attribute and extract details
            .Where(static m => m != null); // Filter out errors that we don't care about

        // Generate source code for each class found
        context.RegisterSourceOutput(classesToGenerate, static (spc, res) => Execute(spc, res));
    }

    private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    {
        return node is ClassDeclarationSyntax m && m.AttributeLists.Count > 0;
    }

    private static ModelTransformerRegistrationModel? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a ClassDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var hostClass = (ClassDeclarationSyntax)context.Node;

        var host = GetHostClass(context.SemanticModel, hostClass);

        if (host != null)
        {
            var attributes = new List<INamedTypeSymbol>();

            // loop through all the attributes on the method
            foreach (var attributeListSyntax in hostClass.AttributeLists)
            {
                foreach (var attributeSyntax in attributeListSyntax.Attributes)
                {
                    if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                    {
                        // weird, we couldn't get the symbol, ignore it
                        continue;
                    }

                    var attributeTypeSymbol = attributeSymbol.ContainingType;
                    var name = attributeTypeSymbol.OriginalDefinition.ToDisplayString();

                    // Is the attribute the [TransformerRegistrationAttribute<T1, T2>] attribute?
                    if ("SourceGeneratorExperiments.Transformers.TransformerRegistrationAttribute<T1, T2>".Equals(name, StringComparison.Ordinal))
                    {
                        attributes.Add(attributeTypeSymbol);
                    }
                }
            }
        }

        // we didn't find the attribute we were looking for
        return null;
    }

    private static void Execute(SourceProductionContext context, ModelTransformerRegistrationModel? tr)
    {
        if (tr != null)
        {
            // generate the source code and add it to the output
            //var result = ModelTransformerIncrementalGeneratorHelper.GenerateExtensionClass(tr);

            // Create a separate partial class file for each class
            //context.AddSource($"{tr.Host.Name}.g.cs", SourceText.From(result, Encoding.UTF8));
        }
    }

    static INamedTypeSymbol? GetHostClass(SemanticModel semanticModel, SyntaxNode classDeclarationSyntax)
    {
        // Get the semantic representation of the class syntax
        if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
        {
            // something went wrong
            return null;
        }

        return classSymbol;
    }
}
