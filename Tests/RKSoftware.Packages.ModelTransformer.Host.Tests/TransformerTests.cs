using RKSoftware.Packages.ModelTransformer.Host.TestSuite;

namespace RKSoftware.Packages.ModelTransformer.Host.Tests;

public class TransformerTests
{
    [Fact]
    public void DomainToViewModelTransformerTest()
    {
        var domain = new Domain
        {
            StringOptional = "John Doe",
            IntOptional = 30,
            StringRequired = "johndoe",
            IntRequired = 42,
            StringIgnored = "Ignore me",
            StringToInt = "123",
            NullableStringToString = "NullableValue",
            IntToNullableInt = 55,
            StringRequiredToNullableString = "RequiredToNullable"
        };

        var viewModel = domain.Transform((ViewModel?)null);

        Assert.Equal(domain.StringOptional, viewModel.StringOptional);

        Assert.Equal(domain.IntOptional, viewModel.IntOptional);

        Assert.Equal(domain.StringRequired, viewModel.StringRequired);

        Assert.Equal(domain.IntRequired, viewModel.IntRequired);

        Assert.True(string.IsNullOrEmpty(viewModel.StringIgnored));

        Assert.False(viewModel.IntIgnored.HasValue);

        Assert.False(string.IsNullOrEmpty(viewModel.StringMissed));

        Assert.True(viewModel.IntMissed.HasValue);

        Assert.Equal(123, viewModel.StringToInt);

        Assert.Equal(domain.NullableStringToString, viewModel.NullableStringToString);

        Assert.Equal(domain.StringRequiredToNullableString, viewModel.StringRequiredToNullableString);

        Assert.Equal(domain.IntToNullableInt, viewModel.IntToNullableInt);
    }

    [Fact]
    public void DomainToDtoTransformerTest()
    {
        var domain = new Domain
        {
            StringOptional = "John Doe",
            IntOptional = 30,
            StringRequired = "johndoe",
            IntRequired = 42,
            StringIgnored = "Ignore me",
            IntToNullableInt = 55,
            StringRequiredToNullableString = "RequiredToNullable"
        };

        var dto = domain.Transform((Dto?)null);

        Assert.Equal(domain.StringOptional, dto.StringOptional);

        Assert.Equal(domain.IntOptional, dto.IntOptional);

        Assert.Equal(domain.StringRequired, dto.StringRequired);

        Assert.Equal(domain.IntRequired, dto.IntRequired);

        Assert.True(string.IsNullOrEmpty(dto.StringIgnored));

        Assert.False(dto.IntIgnored.HasValue);

        Assert.False(string.IsNullOrEmpty(dto.StringMissed));

        Assert.True(dto.IntMissed.HasValue);

        Assert.Equal(domain.StringRequiredToNullableString, dto.StringRequiredToNullableString);

        Assert.Equal(domain.IntToNullableInt, dto.IntToNullableInt);
    }

    [Fact]
    public void DomainToRecordTransformerTest()
    {
        var domain = new Domain
        {
            StringOptional = "John Doe",
            IntOptional = 30,
            StringRequired = "johndoe",
            IntRequired = 42,
            StringIgnored = "Ignore me",
            NullableStringToString = "NullableValue",
            IntToNullableInt = 55,
            StringRequiredToNullableString = "RequiredToNullable"
        };

        var record = domain.Transform((RecordModel?)null);

        Assert.Equal(domain.StringOptional, record.StringOptional);

        Assert.Equal(domain.IntOptional, record.IntOptional);

        Assert.Equal(domain.StringRequired, record.StringRequired);

        Assert.Equal(domain.IntRequired, record.IntRequired);

        Assert.True(string.IsNullOrEmpty(record.StringIgnored));

        Assert.False(record.IntIgnored.HasValue);

        Assert.False(string.IsNullOrEmpty(record.StringMissed));

        Assert.True(record.IntMissed.HasValue);

        Assert.Equal(domain.NullableStringToString, record.NullableStringToString);

        Assert.Equal(domain.StringRequiredToNullableString, record.StringRequiredToNullableString);

        Assert.Equal(domain.IntToNullableInt, record.IntToNullableInt);
    }
}
