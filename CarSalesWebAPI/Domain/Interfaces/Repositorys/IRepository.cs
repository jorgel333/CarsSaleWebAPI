using CarSalesWebAPI.Domain.Entities;
using System.Linq.Expressions;

namespace CarSalesWebAPI.Domain.Interfaces.Repositorys
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> GetAllActive();
        IQueryable<T> GetAllFilter(Expression<Func<T, bool>> predicate);
        Task<T> GetById(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        void Add(T entity);
        void UpdateEntity(T entity);
        void SoftDelete(T entity);
    }
}
