using Exam.App.Services;
using Exam.App.Services.Dtos.CageDTOs.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CageController : Controller
    {
        private readonly ICageService _cageService;

        public CageController(ICageService cageService)
        {
            _cageService = cageService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _cageService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneByIdAsync(int id)
        {
            var result = await _cageService.GetOneById(id);

            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]

        public async Task<IActionResult> AddCage(CageCreateRequestDto cagelDto)
        {
            var result = await _cageService.AddAsync(cagelDto);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<IActionResult> UpdateAnimal(CageUpdateRequestDto cagelDto)
        {
            var result = await _cageService.UpdateAsync(cagelDto);
            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            await _cageService.DeleteAsync(id);
            return NoContent();
        }
    }
}
