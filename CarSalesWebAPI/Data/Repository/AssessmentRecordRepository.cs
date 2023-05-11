using CarSalesWebAPI.Domain.Entities;
using CarSalesWebAPI.Domain.Interfaces.Repositorys;
using Microsoft.EntityFrameworkCore;

namespace CarSalesWebAPI.Data.Repository
{
    public class AssessmentRecordRepository : Repository<AssessmentRecord>, IAssessmentRecordRepository
    {
        public AssessmentRecordRepository(AppDbContext context) : base(context)
        {
        }

    }
}
