using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Onion.Application.Dtos;
using Onion.Application.Services.Interface;
using Onion.Domain.Models;
using Onion.Infrastructures.UnitOfWork.Interface;

namespace Onion.Application.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateStudentAsync(StudentDto studentDto)
        {
            var student = new Student()
            {
                Name = studentDto.Name,
                Email = studentDto.Email,
                FacultyId = studentDto.FacultyId,
                Phone = studentDto.Phone,
            };
            await _unitOfWork.Student.Insert(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _unitOfWork.Student.GetById(id);
            if (student == null)
            {
                throw new Exception("Student doesnot found");
            }
            _unitOfWork.Student.Remove(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var listOfStudents = await _unitOfWork.Student.GetAll();
            return listOfStudents.Select(x => new StudentDto
            {
                Id = x.Id,  
                Email = x.Email,
                Phone = x.Phone,
                Name = x.Name,
                FacultyId = x.FacultyId,
                Faculty = new FacultyDto()
                {
                    Id = x.Faculty!.Id,
                    Name = x.Faculty.Name,
                }
            });
        }

        public async Task<StudentDto> GetStudentByIdAsync(int id)
        {
            var student = await _unitOfWork.Student.GetById(id);
            return new StudentDto
            {
                Id = student.Id,
                Email = student.Email,
                Phone = student.Phone,
                Name = student.Name,
                FacultyId = student.FacultyId,
                Faculty = new FacultyDto()
                {
                    Id = student.Faculty!.Id,
                    Name = student.Faculty.Name,
                }
            };
        }

        public async Task UpdateStudentAsync(int id, StudentDto studentDto)
        {
            var student = await _unitOfWork.Student.GetById(id);
            if (student == null)
            {
                throw new Exception("Student could not found");
            }
            student.Name = studentDto.Name;
            student.Email = studentDto.Email;
            student.FacultyId = studentDto.FacultyId;
            student.Phone = studentDto.Phone;
            await _unitOfWork.SaveAsync();
        }
    }
}
