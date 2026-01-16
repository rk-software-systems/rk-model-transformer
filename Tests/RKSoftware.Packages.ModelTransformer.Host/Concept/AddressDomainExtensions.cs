namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

public static partial class AddressDomainExtensions
{
    public static AddressViewModel Transform(this AddressDomain source, AddressViewModel? target = null)
    {
        ArgumentNullException.ThrowIfNull(source, nameof(source));

        var street = ToViewModelStreetDefault(source);
        ToViewModelStreet(source, ref street);

        if (target == null)
        {
            target = new AddressViewModel
            {
                Street = street
            };
        }
        else
        {
            target.Street = street;
        }

        return target;
    }

    private static string ToViewModelStreetDefault(AddressDomain source)
    {
        return source.Street;
    }
    static partial void ToViewModelStreet(AddressDomain source, ref string street);
}