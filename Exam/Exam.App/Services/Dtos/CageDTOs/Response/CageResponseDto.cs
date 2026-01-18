using Exam.App.Domain.Models;

namespace Exam.App.Services.Dtos.CageDTOs.Response
{
    public class CageResponseDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public ICollection<AnimalSpecies>? CagedAnimals { get; set; } = new List<AnimalSpecies>();
    }
}
