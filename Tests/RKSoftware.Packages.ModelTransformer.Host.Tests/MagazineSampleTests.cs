using RKSoftware.Packages.ModelTransformer.Host.MagazineSample;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests;

public class MagazineSampleTests
{
    private const string _serialYear = "2026";
    private const string _serialNumber = "9876543210";

    [Fact]
    public void SourceModelToDestinationModelTransformerTest()
    {
        var source = new SourceModel
        {
            LicensePlate = "ABC123",
            SerialNumber = $"{_serialYear}-{_serialNumber}",
            Id = SourceModelExtensions.MyId,
            Part1 = "PartOne",
            Part2 = "PartTwo",
            CurrentName = "CurrentNameValue",
            DoNotMap = "ShouldNotBeMapped",
            NotMappedInSource = 42,
            Nested = new SourceNestedModel
            {
                City = "SampleCity",
                Country = "SampleCountry",
                Street = "SampleStreet",
            },
            EnumProperty = "Work",
            Items =
            [
                new SourceListItem { Id = 11, Value = "value11" },
                new SourceListItem { Id = 12, Value = "value12" },
            ],
            Dict = new Dictionary<int, string>
            {
                [1] = "One",
                [2] = "Two",
            }
        };

        var destination = source.Transform();

        Assert.NotNull(destination);

        Assert.Equal(source.LicensePlate, destination.LicensePlate);

        Assert.Equal(_serialNumber, destination.SerialNumber);

        Assert.Equal(_serialYear, destination.SerialYear);

        Assert.Equal(source.Id, destination.Id);

        Assert.Equal(destination.Combined, $"{source.Part1} {source.Part2}");

        Assert.Equal(SourceModelExtensions.MyConstant, destination.ConstValue);

        Assert.Equal(source.Id == SourceModelExtensions.MyId, destination.EvaluatedFromExternal);

        Assert.Equal(source.CurrentName, destination.DifferentName);

        Assert.Equal(source.Nested.Street, destination.Nested.Street);
        Assert.Equal(source.Nested.City, destination.Nested.City);
        Assert.Equal(source.Nested.Country, destination.Nested.Country);

        Assert.Equal(source.EnumProperty, destination.EnumProperty.ToString());

        Assert.Equal(source.Items.Count, destination.Items.Count);
        for (var i = 0; i < destination.Items.Count; i ++)
        {
            Assert.Equal(source.Items[i].Id, destination.Items[i].Id);
            Assert.Equal(source.Items[i].Value, destination.Items[i].Value);
        }

        Assert.Equal(source.Dict.Count, destination.Dict.Count);
        foreach (var kvp in source.Dict)
        {
            Assert.True(destination.Dict.ContainsKey(kvp.Key));
            Assert.Equal(kvp.Value, destination.Dict[kvp.Key]);
        }

        Assert.Null(destination.DoNotMap);
        Assert.Null(destination.NotMappedInDestination);
    }
}
