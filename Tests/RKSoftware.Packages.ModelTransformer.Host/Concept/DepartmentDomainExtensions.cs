namespace RKSoftware.Packages.ModelTransformer.Host.Concept;

public static class DepartmentDomainExtensions
{
    public static DepartmentViewModel Transform(this DepartmentDomain domain, DepartmentViewModel? target = null)
    {
        ArgumentNullException.ThrowIfNull(domain, nameof(domain));

        if (target == null)
        {
            target = new DepartmentViewModel
            {
                Id = domain.Id,
                Name = domain.Name
            };
        }
        else
        {
            target.Id = domain.Id;
            target.Name = domain.Name;


        }

        return target;
    }
}
