using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Interfaces.Repositorys;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarSalesWebAPI.Data.Repository
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly DbSet<Car> _dbSet;
        public CarRepository(AppDbContext context) : base(context)
        {
            _dbSet = context.Set<Car>();    
        }

        public async Task<IEnumerable<Car>> GetAllOrder()
        {
            return await GetAllActive().OrderBy(car => car.Assessments.Count())
                                        .ThenBy(car => car.Model).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetByBrand(string brand)
        {
            return await GetAllFilter(car => car.Brand!.ToLower() == brand.ToLower()
                    && car.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetByModel(string model)
        {
            return await GetAllFilter(car => car.Model!.ToLower() == model.ToLower()).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetByType(string type)
        {
            return await GetAllFilter(car => car.Type!.ToLower() == type.ToLower()).ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetByYearOfManufacture(int year)
        {
            return await GetAllFilter(car => car.YearOfManufacture! == year).ToListAsync();
        }

        public IEnumerable<Car> GetAllToAssessments()
        {
            return GetAllActive().Include(x => x.Assessments)
                .AsQueryable().OrderBy(car => car.Assessments.Count())
                .ThenBy(car => car.Model).ToList();
        }

        public async Task<Car> GetByIdToAssessments(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(x => x.Assessments)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false, cancellationToken);
        }
    }
}
