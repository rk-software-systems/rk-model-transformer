namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ComplexStructure;

public struct IndustryModel : IEquatable<IndustryModel>
{
    public required int Id { get; set; }

    public string? Name { get; set; }

    public override readonly bool Equals(object? obj)
    {
        if (obj is IndustryModel other)
        {
            return Id == other.Id;
        }
        return false;
    }

    public override readonly int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(IndustryModel left, IndustryModel right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(IndustryModel left, IndustryModel right)
    {
        return !(left == right);
    }

    public readonly bool Equals(IndustryModel other)
    {
        return Id == other.Id;
    }
}
