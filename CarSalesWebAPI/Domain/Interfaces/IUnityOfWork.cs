using CarSalesWebAPI.Domain.Interfaces.Repositorys;

namespace CarSalesWebAPI.Domain.Interfaces
{
    public interface IUnityOfWork
    {
        ICarRepository CarRepository { get; }
        IUserRepository UserRepository { get; }
        IAssessmentRecordRepository AssessmentRecordRepository { get; }
        Task Commit(CancellationToken cancellationToken);
    }
}
