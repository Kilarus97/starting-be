using System.Security.Claims;
using AutoMapper;
using BookstoreApplication.DTO;
using Exam.App.Domain;
using Exam.App.Domain.Interface;
using Exam.App.Domain.Models;
using Exam.App.Services.Dtos;
using Exam.App.Services.Dtos.AnimalDTOs.Request;
using Exam.App.Services.Dtos.AnimalDTOs.Response;
using Exam.App.Services.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Exam.App.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public AnimalService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IEnumerable<AnimalResponseDto>> GetAllAsync()
        {
            IEnumerable<AnimalSpecies> animals = await _unitOfWork.AnimalRepository.GetAllWithCages();
            return _mapper.Map<List<AnimalResponseDto>>(animals.ToList());
        }


        public async Task<List<AnimalResponseDto>> SearchAnimalDetailsAsync(AnimalSearchDto search)
        {
            try
            {
                var result = await _unitOfWork.AnimalRepository.SearchAnimalDetailsAsync(search);
                return _mapper.Map<List<AnimalResponseDto>>(result.ToList());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Došlo je do greške prilikom dobavljanja životinje.", ex);
            }
        }


        public async Task<AnimalResponseDto> GetOneById(int id)
        {
            try
            {
                var animal = await _unitOfWork.AnimalRepository.GetOneWithCage(id);

                if (animal == null)
                {
                    throw new NotFoundException(id);
                }

                // mapiranje entiteta u DTO
                var dto = _mapper.Map<AnimalResponseDto>(animal); 
                return dto;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Došlo je do greške prilikom dobavljanja životinje.", ex);
            }
        }


        public async Task<AnimalResponseDto> AddAsync(AnimalCreateRequestDto animalDto)
        {
            try
            {
                var animal = _mapper.Map<AnimalSpecies>(animalDto);

                if (animalDto.CageId != null)
                {
                    var cage = await _unitOfWork.CagesRepository.GetOneAsync(animalDto.CageId.Value);

                    if (cage == null)

                        throw new NotFoundException(animalDto.CageId.Value);
                }

                animal.CageId = animalDto.CageId;

                await _unitOfWork.AnimalRepository.AddAsync(animal);

                await _unitOfWork.CompleteAsync();

                return _mapper.Map<AnimalResponseDto>(animal);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Došlo je do greške prilikom dodavanja životinje.", ex);
            }
        }

        public async Task<AnimalResponseDto> UpdateAsync(AnimalUpdateRequestDto animalDto)
        {
            try
            {
                var animal = await _unitOfWork.AnimalRepository.GetOneAsync(animalDto.Id);

                if (animal == null)
                    throw new NotFoundException(animalDto.Id);

                if (animalDto.CageId != null)
                {
                    var cage = await _unitOfWork.CagesRepository.GetOneAsync(animalDto.CageId.Value);

                    if (cage == null)
                        throw new NotFoundException(animalDto.CageId.Value);

                    // 🚨 Provera: da li je životinja već u tom kavezu
                    if (animal.CageId == animalDto.CageId)
                        throw new InvalidOperationException(
                            $"Životinja sa ID {animal.Id} je već smeštena u kavez {animalDto.CageId}.");
                }

                _mapper.Map(animalDto, animal);

                // Ručno dodeli CageId jer AutoMapper može da preskoči null vrednosti
                animal.CageId = animalDto.CageId;

                _unitOfWork.AnimalRepository.Update(animal);
                await _unitOfWork.CompleteAsync();

                return _mapper.Map<AnimalResponseDto>(animal);
            }
            catch (NotFoundException)
            {
                throw; // propagiraj dalje
            }
            catch (InvalidOperationException)
            {
                throw; // propagiraj dalje
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Došlo je do greške prilikom izmene životinje.", ex);
            }
        }


        public async Task DeleteAsync(int id)
        {
            try
            {
                var animal = await _unitOfWork.AnimalRepository.GetOneAsync(id);

                if (animal == null)
                {
                    throw new NotFoundException(id);
                }

                _unitOfWork.AnimalRepository.Delete(animal);

                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Došlo je do greške prilikom dodavanja životinje.", ex);
            }
        }
    }
}
