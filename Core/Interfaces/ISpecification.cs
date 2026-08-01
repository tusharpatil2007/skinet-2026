using System.Linq.Expressions;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria {get;}

    Expression<Func<T,object>>? OrderBy {get;}
    Expression<Func<T,object>>? OrderByDescending {get;}   

    bool IsDistnct {get;} 
}

public interface ISpecification<T, Tresult> : ISpecification<T>
{
    Expression<Func<T, Tresult>>? Select {get;}
}