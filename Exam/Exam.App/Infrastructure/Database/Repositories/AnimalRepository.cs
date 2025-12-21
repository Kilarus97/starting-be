using Delivery.Infrastructure.Repositories;
using Exam.App.Domain.Interface;
using Exam.App.Domain.Models;

namespace Exam.App.Infrastructure.Database.Repositories
{
    public class AnimalRepository : GenericRepository<AnimalSpecies>, IAnimalRepository
    {
        public AnimalRepository(AppDbContext dbContext) : base(dbContext) { }

    }
}
