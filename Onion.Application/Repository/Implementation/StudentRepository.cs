using Microsoft.EntityFrameworkCore;
using Onion.Application.Dtos;
using Onion.Domain.Models;
using Onion.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Repository.Interface
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<StudentDto>> GetAllStudentsWithFaculty()
        {
            return await _context.Students.Select(x=> new StudentDto()
            {
                Id= x.Id,
                Name= x.Name,
                Email= x.Email,
                Phone= x.Phone,
                Faculty = new FacultyDto() 
                {
                    Id = x.Faculty!.Id,
                    Name = x.Faculty.Name,
                }
            }).ToListAsync();
        }

        public async Task<StudentDto> GetStudentWithFacultyById(int id)
        {
            var studentDto = await _context.Students.Select(x=>new StudentDto()
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Faculty = new FacultyDto()
                {
                    Id = x.Faculty!.Id,
                    Name = x.Faculty.Name,
                }
            }).FirstOrDefaultAsync(x => x.Id == id);
            return studentDto!;
        }
    }
}
