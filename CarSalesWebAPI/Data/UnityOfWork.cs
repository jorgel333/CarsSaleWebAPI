using CarSalesWebAPI.Data.Repository;
using CarSalesWebAPI.Domain.Interfaces;
using CarSalesWebAPI.Domain.Interfaces.Repositorys;

namespace CarSalesWebAPI.Data
{
    public class UnityOfWork : IUnityOfWork
    {
        private CarRepository _carRepository;
        private UserRepository _userRepository;
        private AssessmentRecordRepository _assessmentRecordRepository;
        public AppDbContext _context;
        public UnityOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICarRepository CarRepository
        {
            get
            {
                return _carRepository = _carRepository ?? new CarRepository(_context);
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_context);
            }
        }

        public IAssessmentRecordRepository AssessmentRecordRepository
        {
            get
            {
                return _assessmentRecordRepository = _assessmentRecordRepository ?? new AssessmentRecordRepository(_context);
            }
        }

        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()   
        {
            _context.Dispose();
        }
    }
}
