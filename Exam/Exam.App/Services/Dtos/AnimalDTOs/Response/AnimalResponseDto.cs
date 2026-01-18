using Exam.App.Domain.Models;

namespace Exam.App.Services.Dtos.AnimalDTOs.Response
{
    public class AnimalResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double? Mass { get; set; }
        public Cage Cage { get; set; }
    }
}
