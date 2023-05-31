using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Interfaces.Repositorys;
using CarSalesWebAPI.Domain.Pagination;
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
            return await GetAllActive().OrderByDescending(car => car.Average)
                                        .ThenByDescending(car => car.Assessments.Count())
                                        .ThenBy(car => car.Model).ToListAsync();
        }

        public async Task<PagedList<Car>> GetPagination(CarsParameters carsParameters, CancellationToken cancellationToken)
        {
            var cars = GetAllActive().OrderByDescending(car => car.Average).ThenByDescending(car => car.Assessments.Count());
            return await PagedList<Car>.ToPagedList(cars, carsParameters.PageNumber, carsParameters.PageSize);
        }
        public async Task<Car> GetByIdToAssessments(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(x => x.Assessments)
                .FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted == false, cancellationToken);
        }

        public async Task<IEnumerable<Car>> GetCarsFilter(string model, string brand, string type, int? year, CancellationToken cancellationToken)
        {
            var query = GetAllActive();

            if (!string.IsNullOrEmpty(model))
            {
                query = query.Where(car => car.Model!.ToLower() == model.ToLower());
            }

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(car => car.Brand!.ToLower() == brand.ToLower());
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(car => car.Type!.ToLower() == type.ToLower());
            }

            if (year.HasValue)
            {
                query = query.Where(car => car.YearOfManufacture! == year);
            }

            return await query.OrderByDescending(car => car.Average)
                                        .ThenByDescending(car => car.Assessments.Count())
                                        .ThenBy(car => car.Model).ToListAsync(cancellationToken);
        }
        public void UpdateAverage(Car car)
        {
            car.Average = car.Assessments.Select(x => x.Note).Average();
            UpdateEntity(car);
        }

    }
}
