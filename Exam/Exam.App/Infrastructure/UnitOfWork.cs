using Exam.App.Domain.Interface;
using Exam.App.Infrastructure.Database;
using Exam.App.Infrastructure.Database.Repositories;

namespace Exam.App.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IAnimalRepository AnimalRepository { get; set; }
        public ICagesRepository CagesRepository { get; set; }
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            AnimalRepository = new AnimalRepository(dbContext);
            CagesRepository = new CagesRepository(dbContext);

        }

        public Task<int> CompleteAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
