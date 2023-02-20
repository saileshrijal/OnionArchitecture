using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Application.Dtos;
using Onion.Application.Services.Interface;
using Onion.Presentation.ViewModels;

namespace Onion.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var studentsDto = await _studentService.GetAllStudentsAsync();
                var studentsVM = studentsDto.Select(x => new StudentVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    FacultyId = x.FacultyId,
                    Faculty = new FacultyVM()
                    {
                        Id = x.Faculty!.Id,
                        Name = x.Faculty.Name,
                    }
                });
                return Ok(studentsVM.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone,
                    Faculty = x.Faculty,
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var studentDto = await _studentService.GetStudentByIdAsync(id);
                var studentVM = new StudentVM()
                {
                    Id = studentDto.Id,
                    Name = studentDto.Name,
                    Email = studentDto.Email,
                    Phone = studentDto.Phone,
                    FacultyId = studentDto.FacultyId,
                    Faculty = new FacultyVM()
                    {
                        Id = studentDto.Faculty!.Id,
                        Name = studentDto.Faculty.Name,
                    }
                };
                return Ok(studentVM);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentVM studentVM)
        {
            try
            {
                var studentDto = new StudentDto() 
                { 
                    Name = studentVM.Name,
                    Email = studentVM.Email,
                    Phone = studentVM.Phone,
                    FacultyId = studentVM.FacultyId
                };
                await _studentService.CreateStudentAsync(studentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] StudentVM studentVM)
        {
            try
            {
                var stduentDto = new StudentDto()
                {
                    Name = studentVM.Name,
                    Email = studentVM.Email,
                    Phone = studentVM.Phone,
                    FacultyId = studentVM.FacultyId
                };
                await _studentService.UpdateStudentAsync(id, stduentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
