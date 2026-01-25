
namespace RKSoftware.Packages.ModelTransformer.Host.MagazineSample;

public static partial class SourceModelExtensions
{
    public const long MyId = 1;
    public const int MyConstant = 100;

    private static partial string ToDestinationModelCombined(SourceModel source)
    {
        return $"{source.Part1} {source.Part2}";
    }

    private static partial long ToDestinationModelConstValue(SourceModel source)
    {
        return MyConstant;
    }

    private static partial bool ToDestinationModelEvaluatedFromExternal(SourceModel source)
    {
        return source.Id == MyId;
    }

    private static partial string ToDestinationModelDifferentName(SourceModel source)
    {
        return source.CurrentName;
    }

    private static partial AddressKinds ToDestinationModelEnumProperty(SourceModel source)
    {
        return Enum.Parse<AddressKinds>(source.EnumProperty);
    }

    private static partial Dictionary<int, string> ToDestinationModelDict(SourceModel source)
    {
        return new Dictionary<int, string>(source.Dict);
    }

    private static partial string ToDestinationModelSerialYear(SourceModel source)
    {
        return source.SerialNumber.Split('-')[0];
    }

    private static partial string ToDestinationModelSerialNumber(SourceModel source)
    {
        return source.SerialNumber.Split('-')[1];
    }
}
