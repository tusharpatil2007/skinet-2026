using Core.Entities;

public class BrandListSpecification : BaseSpecification<Product, String>
{
    public BrandListSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();
    }
}