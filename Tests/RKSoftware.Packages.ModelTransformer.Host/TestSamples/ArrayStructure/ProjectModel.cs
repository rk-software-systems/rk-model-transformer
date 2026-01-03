namespace RKSoftware.Packages.ModelTransformer.Host.TestSamples.ArrayStructure;

public struct ProjectModel : IEquatable<ProjectModel>
{
    public required int Id { get; set; }

    public string? Name { get; set; }

    public override readonly bool Equals(object? obj)
    {
        if (obj is ProjectModel other)
        {
            return Id == other.Id;
        }
        return false;
    }

    public override readonly int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(ProjectModel left, ProjectModel right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ProjectModel left, ProjectModel right)
    {
        return !(left == right);
    }

    public readonly bool Equals(ProjectModel other)
    {
        return Id == other.Id;
    }
}
