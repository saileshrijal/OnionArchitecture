using Onion.Application.Dtos;
using Onion.Application.Services.Interface;
using Onion.Domain.Models;
using Onion.Infrastructures.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Services.Implementation
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FacultyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateFacultyAsync(FacultyDto facultyDto)
        {
            var faculty = new Faculty()
            {
                Name = facultyDto.Name,
            };
            await _unitOfWork.Faculty.Insert(faculty);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFacultyAsync(int id)
        {
            var faculty = await _unitOfWork.Faculty.GetById(id);
            if(faculty== null)
            {
                throw new Exception("Faculty doesnot found");
            }
            _unitOfWork.Faculty.Remove(faculty);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<FacultyDto>> GetAllFacultiesAsync()
        {
            var listOfFaculties = await _unitOfWork.Faculty.GetAll();
            return listOfFaculties.Select(x => new FacultyDto
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        public async Task<FacultyDto> GetFacultyByIdAsync(int id)
        {
            var faculty = await _unitOfWork.Faculty.GetById(id);
            return new FacultyDto 
            {
                Id = faculty.Id,
                Name = faculty.Name,
            };
        }

        public async Task UpdateFacultyAsync(int id, FacultyDto facultyDto)
        {
            var faculty = await _unitOfWork.Faculty.GetById(id);
            if (faculty == null)
            {
                throw new Exception("Faculty could not found");
            }
            faculty.Name = facultyDto.Name;
            await _unitOfWork.SaveAsync();
        }
    }
}
