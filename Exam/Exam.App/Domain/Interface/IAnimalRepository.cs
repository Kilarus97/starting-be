using Delivery.Infrastructure.Repositories;
using Exam.App.Domain.Models;

namespace Exam.App.Domain.Interface
{
    public interface IAnimalRepository : IGenericRepository<AnimalSpecies>
    {
        Task<List<AnimalSpecies>> GetAllWithCages();
        Task<AnimalSpecies> GetOneWithCage(int Id);
    }
}
