using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Onion.Application.Dtos;
using Onion.Application.Services.Interface;
using Onion.Domain.Models;
using Onion.Infrastructures.Repository.Interface;
using Onion.Infrastructures.UnitOfWork.Interface;

namespace Onion.Application.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _studentRepository;

        public StudentService(IUnitOfWork unitOfWork, IStudentRepository studentRepository)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
        }

        public async Task CreateStudentAsync(StudentDto studentDto)
        {
            var student = new Student()
            {
                Name = studentDto.Name,
                Email = studentDto.Email,
                Phone = studentDto.Phone,
                FacultyId= studentDto.FacultyId,
            };
            await _unitOfWork.CreateAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetById(id);
            if(student == null) { throw new Exception("Student not found"); }
            _unitOfWork.Remove(student);
            _unitOfWork.Save();
        }

        public async Task UpdateStudentAsync(int id, StudentDto studentDto)
        {
            var student = await _studentRepository.GetById(id);
            if(student == null ) { throw new Exception("Student not found"); }
            student.Name = studentDto.Name;
            student.Email = studentDto.Email;
            student.Phone = studentDto.Phone;
            student.FacultyId= studentDto.FacultyId;
            _unitOfWork.Save();
        }
    }
}
