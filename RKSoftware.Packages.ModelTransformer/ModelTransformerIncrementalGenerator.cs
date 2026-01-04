using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using RKSoftware.Packages.ModelTransformer.Builders;
using RKSoftware.Packages.ModelTransformer.Extensions;
using RKSoftware.Packages.ModelTransformer.Models;

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
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            $"{RegistrationAttributeCodeBuilder.Name}.g.cs", 
            SourceText.From(RegistrationAttributeCodeBuilder.SourceCode, Encoding.UTF8))
        );

        IncrementalValuesProvider<RegistrationModel?> classesToGenerate = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // select classes with attributes
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // select classes with the marker attribute and extract details
            .Where(static m => m != null); // Filter out errors that we don't care about

        // Generate source code for each class found
        context.RegisterSourceOutput(classesToGenerate, static (spc, res) => Execute(spc, res));
    }

    private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
    {
        return node is ClassDeclarationSyntax m && m.AttributeLists.Count > 0;
    }

    private static RegistrationModel? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a ClassDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var hostClass = (ClassDeclarationSyntax)context.Node;

        var host = GetHostClass(context.SemanticModel, hostClass);

        if (host != null)
        {
            var registrationModel = new RegistrationModel(host);

            // loop through all the attributes on the method
            foreach (var attributeData in host.GetAttributes())
            {
                var attributeClass = attributeData.AttributeClass;
                if (attributeClass == null)
                {
                    continue;
                }

                var name = attributeClass.OriginalDefinition.ToDisplayString();                               

                // Is the attribute the marker attribute?
                if ($"{RegistrationAttributeCodeBuilder.Namespace}.{RegistrationAttributeCodeBuilder.GenericName}".Equals(name, StringComparison.Ordinal))
                {
                    var attr = new AttributeDataModel(registrationModel.HostNamespace, attributeData);
                    registrationModel.Attributes.Add(attr);
                }                
            }

            return registrationModel.Attributes.Count > 0 ? registrationModel : null;
        }

        // we didn't find the attribute we were looking for
        return null;
    }

    private static void Execute(SourceProductionContext context, RegistrationModel? tr)
    {
        if (tr != null)
        {
            var groupedBySource = tr.Attributes
                .GroupBy(a =>  a.Key)
                .ToDictionary(x => x.Key, y => y.ToList());

            foreach (var group in groupedBySource)
            {
                var sourceName = group.Value.First().ClassName;
                var extensionClassBuilder = new ExtensionClassCodeBuilder(tr.HostNamespace, sourceName, new List<ExtensionMethodCodeBuilder>(group.Value.Count));

                foreach (var attr in group.Value)
                {
                    var incorrectIgnoredProperties = attr.IncorrectIgnoredProperties;
                    if (incorrectIgnoredProperties.Count > 0)
                    {
                        context.CreateInvalidIgnoredPropertyNameWarning(tr.Location, attr.Source, attr.Target, incorrectIgnoredProperties);
                    }

                    var notIgnoredReadonlyProperties = attr.NotIgnoredReadonlyProperties;
                    if (notIgnoredReadonlyProperties.Count > 0)
                    {
                        context.CreateReadonlyPropertyMustBeIgnoredWarning(tr.Location, attr.Source, attr.Target, notIgnoredReadonlyProperties);
                    }

                    var notNullableIgnoredProperties = attr.NotNullableIgnoredProperties;
                    if (notNullableIgnoredProperties.Count > 0)
                    {
                        context.CreateNotNullablePropertyCanNotBeIgnoredWarning(tr.Location, attr.Source, attr.Target, notNullableIgnoredProperties);
                    }

                    var incorrectPropertiesWithoutDefaultMapping = attr.IncorrectPropertiesWithoutDefaultMapping;
                    if (incorrectPropertiesWithoutDefaultMapping.Count > 0)
                    {
                        context.CreateInvalidPropertyNameWithoutDefaultMappingWarning(tr.Location, attr.Source, attr.Target, incorrectPropertiesWithoutDefaultMapping);
                    }

                    extensionClassBuilder.Methods.Add(new ExtensionMethodCodeBuilder(attr, groupedBySource));
                }

                var extensionClassCode = extensionClassBuilder.Generate();

                var fileName = $"{tr.HostNamespace}.{sourceName}.g.cs";

                context.AddSource(fileName, SourceText.From(extensionClassCode, Encoding.UTF8));
            }
        }
    }

    private static INamedTypeSymbol? GetHostClass(SemanticModel semanticModel, SyntaxNode classDeclarationSyntax)
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
