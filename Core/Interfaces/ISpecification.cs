using System.Linq.Expressions;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria {get;}

    Expression<Func<T,object>>? OrderBy {get;}
    Expression<Func<T,object>>? OrderByDescending {get;}   

    bool IsDistnct {get;}

    int Take {get;}

    int Skip {get;}

    bool IsPagingEnabled{get;} 

    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}

public interface ISpecification<T, Tresult> : ISpecification<T>
{
    Expression<Func<T, Tresult>>? Select {get;}
}