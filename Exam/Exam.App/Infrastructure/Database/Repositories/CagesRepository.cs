using Delivery.Infrastructure.Repositories;
using Exam.App.Domain.Interface;
using Exam.App.Domain.Models;

namespace Exam.App.Infrastructure.Database.Repositories
{
    public class CagesRepository : GenericRepository<Cage>, ICagesRepository
    {
        public CagesRepository(AppDbContext dbContext) : base(dbContext) { }

    }
}
