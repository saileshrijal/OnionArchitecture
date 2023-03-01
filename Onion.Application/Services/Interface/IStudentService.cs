using Onion.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Services.Interface
{
    public interface IStudentService
    {
        Task CreateStudentAsync(StudentDto studentDto);
        Task UpdateStudentAsync(int id, StudentDto studentDto);
        Task DeleteStudentAsync(int id);
    }
}
