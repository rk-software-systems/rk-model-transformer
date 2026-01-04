using System.Globalization;

namespace RKSoftware.Packages.ModelTransformer.Host.RandomSample;

public static partial class DomainExtensions
{
    private static partial string? ToViewModelStringMissed(Domain source)
    {
        return "DefaultMissedValue";
    }

    private static partial int? ToViewModelIntMissed(Domain source)
    {
        return 42;
    }

    private static partial int? ToViewModelStringToInt(Domain source)
    {
        return string.IsNullOrEmpty(source.StringToInt) ? null : int.Parse(source.StringToInt, CultureInfo.InvariantCulture);
    }

    private static partial string ToViewModelNullableStringToString(Domain source)
    {
        return source.NullableStringToString ?? string.Empty;
    }

    private static partial string? ToDtoStringMissed(Domain source)
    {
        return "DefaultMissedValue";
    }

    private static partial int? ToDtoIntMissed(Domain source)
    {
        return 42;
    }

    private static partial string? ToRecordModelStringMissed(Domain source)
    {
        return "DefaultMissedValue";
    }

    private static partial int? ToRecordModelIntMissed(Domain source)
    {
        return 42;
    }

    private static partial string ToRecordModelNullableStringToString(Domain source)
    {
        return source.NullableStringToString ?? string.Empty;
    }

    private static partial int ToViewModelIntRequiredIgnored(Domain source)
    {
        return 0;
    }

    private static partial string ToViewModelStringRequiredIgnored(Domain source)
    {
        return string.Empty;
    }

    private static partial string? ToRecordModelWithoutDefaultMapping(Domain source)
    {
        return source.WithoutDefaultMapping; 
    }

    private static partial string? ToViewModelWithoutDefaultMapping(Domain source)
    {
        return source.WithoutDefaultMapping;
    }

    //private static partial string? ToDtoWithoutDefaultMapping(Domain source)
    //{
    //    return source.WithoutDefaultMapping;
    //}
}
