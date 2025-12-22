namespace Exam.App.Domain.Models
{
    public class AnimalSpecies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double? Mass { get; set; }
        public int? CageId { get; set; }
        public Cage? Cage { get; set; }
    }
}
