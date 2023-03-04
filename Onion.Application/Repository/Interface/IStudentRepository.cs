using Onion.Application.Dtos;
using Onion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Repository.Interface
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<List<StudentDto>> GetAllStudentsWithFaculty();
        Task<StudentDto> GetStudentWithFacultyById(int id);
    }
}
