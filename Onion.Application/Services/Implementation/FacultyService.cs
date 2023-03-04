using Onion.Application.Dtos;
using Onion.Application.Repository.Interface;
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
        private readonly IFacultyRepository _facultyRepository;

        public FacultyService(IUnitOfWork unitOfWork, IFacultyRepository facultyRepository)
        {
            _unitOfWork = unitOfWork;
            _facultyRepository = facultyRepository;
        }

        public async Task CreateFacultyAsync(FacultyDto facultyDto)
        {
            var faculty = new Faculty()
            {
                Name = facultyDto.Name
            };
            await _unitOfWork.CreateAsync(faculty);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFacultyAsync(int id)
        {
            var faculty = await _facultyRepository.GetById(id);
            if(faculty == null) { throw new Exception("Faculty not found"); }
            _unitOfWork.Remove(faculty);
            _unitOfWork.Save();
        }

        public async Task UpdateFacultyAsync(int id, FacultyDto facultyDto)
        {
            var faculty = await _facultyRepository.GetById(id);
            if(faculty == null) { throw new Exception("Faculty not found"); }
            faculty.Name = facultyDto.Name;
            _unitOfWork.Save();
        }
    }
}
