using Onion.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Application.Services.Interface
{
    public interface IFacultyService
    {
        Task<IEnumerable<FacultyDto>> GetAllFacultiesAsync();
        Task<FacultyDto> GetFacultyByIdAsync(int id);
        Task CreateFacultyAsync(FacultyDto facultyDto);
        Task UpdateFacultyAsync(int id, FacultyDto facultyDto);
        Task DeleteFacultyAsync(int id);
    }
}
