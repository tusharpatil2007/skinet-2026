using Core.Entities;

public class TypeListSpecification : BaseSpecification <Product, string>
{
    public TypeListSpecification()
    {
        AddSelect(x => x.Type);
        ApplyDistinct();
    }
}