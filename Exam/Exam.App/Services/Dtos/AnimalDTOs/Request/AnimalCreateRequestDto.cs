namespace Exam.App.Services.Dtos.AnimalDTOs.Request
{
    public class AnimalCreateRequestDto
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public double Mass { get; set; }
        public int? CageId { get; set; }
    }
}
