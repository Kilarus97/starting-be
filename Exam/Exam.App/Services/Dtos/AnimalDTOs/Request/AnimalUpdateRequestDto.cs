namespace Exam.App.Services.Dtos.AnimalDTOs.Request
{
    public class AnimalUpdateRequestDto
    {
        public int Id { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public double Mass { get; set; }
        public int? CageId { get; set; }
    }
}
