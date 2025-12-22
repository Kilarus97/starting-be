using Exam.App.Services;
using Exam.App.Services.Dtos;
using Exam.App.Services.Dtos.AnimalDTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class AnimalController : Controller
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _animalService.GetAllAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal(AnimalCreateRequestDto animalDto)
        {
            var result = await _animalService.AddAsync(animalDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAnimal(AnimalUpdateRequestDto animalDto)
        {
            var result = await _animalService.UpdateAsync(animalDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            await _animalService.DeleteAsync(id);
            return NoContent();
        }


    }
}
