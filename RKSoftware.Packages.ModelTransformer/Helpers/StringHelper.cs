namespace RKSoftware.Packages.ModelTransformer.Helpers;
internal class StringHelper
{
    public static string LowerCaseFirstLetter(string input)
    {
        return string.IsNullOrEmpty(input)
            ? string.Empty
            : string.Concat(char.ToLowerInvariant(input[0]), input.Substring(1));
    }
}
