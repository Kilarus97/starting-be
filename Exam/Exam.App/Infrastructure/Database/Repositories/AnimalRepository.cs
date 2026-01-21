using System.Diagnostics.Metrics;
using BookstoreApplication.DTO;
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



        public async Task<List<AnimalSpecies>> SearchAnimalDetailsAsync(AnimalSearchDto search)
        {
            var query = _dbContext.AnimalSpecies
                .Include(a => a.Cage)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Species))
                query = query.Where(a => a.Species.ToLower().Contains(search.Species.ToLower()));


            if (!string.IsNullOrWhiteSpace(search.CageCode))
                query = query.Where(a => a.Cage != null &&
                                         a.Cage.Code.ToLower().Contains(search.CageCode.ToLower()));


            if (search.FilterNonCaged == true)
                query = query.Where(a => a.Cage == null);


            if (!string.IsNullOrWhiteSpace(search.SortType))
            {
                query = search.SortType switch
                {
                    "NameAsc" => query.OrderBy(a => a.Name),
                    "NameDesc" => query.OrderByDescending(a => a.Name),
                    "SpeciesAsc" => query.OrderBy(a => a.Species),
                    "SpeciesDesc" => query.OrderByDescending(a => a.Species),
                    "CageAsc" => query.OrderBy(a => a.Cage.Code),
                    "CageDesc" => query.OrderByDescending(a => a.Cage.Code),
                    _ => query.OrderBy(a => a.Name)
                };
            }

            return await query.ToListAsync();
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
