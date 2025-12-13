namespace RKSoftware.Packages.ModelTransformer.Extensions;

internal static class StringExtensions
{
    public static string LowerCaseFirstLetter(this string input)
    {
        return string.IsNullOrEmpty(input)
            ? string.Empty
            : string.Concat(char.ToLowerInvariant(input[0]), input.Substring(1));
    }

    public static string UpperCaseFirstLetter(this string input)
    {
        return string.IsNullOrEmpty(input)
            ? string.Empty
            : string.Concat(char.ToUpperInvariant(input[0]), input.Substring(1));
    }
}
