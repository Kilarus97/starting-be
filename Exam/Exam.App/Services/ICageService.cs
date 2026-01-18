using Exam.App.Services.Dtos.CageDTOs.Request;
using Exam.App.Services.Dtos.CageDTOs.Response;

namespace Exam.App.Services
{
    public interface ICageService
    {
        Task<IEnumerable<CageResponseDto>> GetAllAsync();
        Task<CageResponseDto> GetOneById(int id);
        Task<CageResponseDto> AddAsync(CageCreateRequestDto cageDto);
        Task<CageResponseDto> UpdateAsync(CageUpdateRequestDto cageDto);
        Task DeleteAsync(int id);
    }
}
