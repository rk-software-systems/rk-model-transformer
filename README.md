# Model Transformer
Helps to transform one model to another.

| Package									|  Information                    |
|-------------------------------------------|--------------------------------|
|  RKSoftware.Packages.ModelTransformer     | [![CI](https://github.com/rk-software-systems/rk-model-transformer/actions/workflows/ci.yml/badge.svg)](https://github.com/rk-software-systems/rk-model-transformer/actions/workflows/ci.yml)

## Setup
1) To install the package, run the following command in the Package Manager Console:
```
Install-Package RKSoftware.Packages.ModelTransformer
```
Or via .NET CLI:
```
dotnet add package RKSoftware.Packages.ModelTransformer
```
Or via NuGet Package Manager, search for `RKSoftware.Packages.ModelTransformer` and install it.

2) Add `OutputItemType="Analyzer" ReferenceOutputAssembly="false"` to the package reference in your project file:
```xml
<PackageReference Include="RKSoftware.Packages.ModelTransformer" Version="x.y.z" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
```

## Usage
1) Add the using directive to your file:
```
using RKSoftware.Packages.ModelTransformer.Attributes;
```

2) Use the `[ModelTransformerRegistration]` attribute to specify source and target models for transformation:
```csharp
[ModelTransformerRegistration<SourceDomain, TargetViewModel>]
public class SourceTransformerRegistration
{
}
```
