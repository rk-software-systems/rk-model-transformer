namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

public static class DepartmentDomainExtensions
{
    public static DepartmentViewModel Transform(this DepartmentDomain domain)
    {
        ArgumentNullException.ThrowIfNull(domain, nameof(domain));

        return new DepartmentViewModel
        {
            Id = domain.Id,
            Name = domain.Name
        };
    }
}
