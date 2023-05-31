using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Interfaces.Repositorys;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarSalesWebAPI.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void SoftDelete(T entity)
        {
            entity.IsDeleted = true;
            UpdateEntity(entity);
        }

        public IQueryable<T> GetAllActive()
        {
            return _dbSet.Where(x => x.IsDeleted == false).AsNoTracking();
        }

        public IQueryable<T> GetAllFilter(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public void UpdateEntity(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _dbSet.Update(entity);
        }

    }
}
