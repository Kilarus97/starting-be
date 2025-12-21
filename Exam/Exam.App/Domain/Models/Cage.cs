namespace Exam.App.Domain.Models
{
    public class Cage
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public ICollection<AnimalSpecies>? CagedAnimals { get; set; } = new List<AnimalSpecies>();
    }
}
