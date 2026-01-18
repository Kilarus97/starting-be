using System.Diagnostics.Metrics;
using Delivery.Infrastructure.Repositories;
using Exam.App.Domain.Interface;
using Exam.App.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.App.Infrastructure.Database.Repositories
{
    public class AnimalRepository : GenericRepository<AnimalSpecies>, IAnimalRepository
    {
        private readonly AppDbContext _dbContext;

        public AnimalRepository(AppDbContext dbContext) : base(dbContext) {

            _dbContext = dbContext;
        }


        public async Task<List<AnimalSpecies>> GetAllWithCages()
        {
            return await _dbContext.AnimalSpecies
                .Include(a => a.Cage)
                .ToListAsync();
        }

        public async Task<AnimalSpecies> GetOneWithCage(int Id)
        {
            return await _dbContext.AnimalSpecies
                .Include(a => a.Cage)
                .FirstOrDefaultAsync(a => a.Id == Id);
        }
    }
}
