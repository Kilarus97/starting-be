using AutoMapper;
using Exam.App.Domain;
using Exam.App.Domain.Interface;
using Exam.App.Domain.Models;
using Exam.App.Services.Dtos.CageDTOs.Request;
using Exam.App.Services.Dtos.CageDTOs.Response;
using Exam.App.Services.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Exam.App.Services
{
    // Ensure CageService implements ICageService
    public class CageService : ICageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CageService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IEnumerable<CageResponseDto>> GetAllAsync()
        {
            IEnumerable<Cage> Cages = await _unitOfWork.CagesRepository.GetAllAsync();
            return _mapper.Map<List<CageResponseDto>>(Cages.ToList());
        }


        public async Task<CageResponseDto> GetOneById(int id)
        {
            try
            {
                var cage = await _unitOfWork.CagesRepository.GetOneAsync(id);

                if (cage == null)
                {
                    throw new NotFoundException(id);
                }

                // mapiranje entiteta u DTO
                var dto = _mapper.Map<CageResponseDto>(cage);
                return dto;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Došlo je do greške prilikom dobavljanja kaveza.", ex);
            }
        }



        public async Task<CageResponseDto> AddAsync(CageCreateRequestDto cageDto)
        {
            try
            {
                var cage = _mapper.Map<Cage>(cageDto);

                await _unitOfWork.CagesRepository.AddAsync(cage);

                await _unitOfWork.CompleteAsync();

                return _mapper.Map<CageResponseDto>(cage);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Došlo je do greške prilikom dodavanja kaveza.", ex);
            }
        }


        public async Task<CageResponseDto> UpdateAsync(CageUpdateRequestDto cageDto)
        {
            try
            {
                var cage = await _unitOfWork.CagesRepository.GetOneAsync(cageDto.Id);

                if (cage == null)
                {
                    throw new NotFoundException(cageDto.Id);
                }
                _mapper.Map(cageDto, cage);

                _unitOfWork.CagesRepository.Update(cage);

                await _unitOfWork.CompleteAsync();

                return _mapper.Map<CageResponseDto>(cage);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Došlo je do greške prilikom dodavanja kaveza.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var cage = await _unitOfWork.CagesRepository.GetOneAsync(id);

                if (cage == null)
                {
                    throw new NotFoundException(id);
                }

                _unitOfWork.CagesRepository.Delete(cage);

                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Došlo je do greške prilikom dodavanja kaveza.", ex);
            }
        }
    }
}
