using BookstoreApplication.DTO;
using Exam.App.Domain.Models;
using Exam.App.Services;
using Exam.App.Services.Dtos;
using Exam.App.Services.Dtos.AnimalDTOs.Request;
using Exam.App.Services.Dtos.AnimalDTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Exam.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneByIdAsync(int id)
        {
            var result = await _animalService.GetOneById(id);

            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("search")]
        public async Task<ActionResult<List<AnimalResponseDto>>> SearchAnimalDetails([FromBody] AnimalSearchDto search)
        {
            var result = await _animalService.SearchAnimalDetailsAsync(search);

            if (result == null || !result.Any())
                return NotFound("Nema pronađenih životinja.");

            return Ok(result);
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]

        public async Task<IActionResult> AddAnimal(AnimalCreateRequestDto animalDto)
        {
            var result = await _animalService.AddAsync(animalDto);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<IActionResult> UpdateAnimal(AnimalUpdateRequestDto animalDto)
        {
            var result = await _animalService.UpdateAsync(animalDto);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            await _animalService.DeleteAsync(id);
            return NoContent();
        }


    }
}
