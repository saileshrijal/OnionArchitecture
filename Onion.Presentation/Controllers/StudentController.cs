using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Application.Dtos;
using Onion.Application.Services.Interface;
using Onion.Infrastructures.Repository.Interface;
using Onion.Presentation.ViewModels;

namespace Onion.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentService studentService, IStudentRepository studentRepository)
        {
            _studentService = studentService;
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var student = await _studentRepository.GetAll();
                var result = student.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Phone,
                    x.Email,
                    x.FacultyId
                });
                return Ok(result);
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
                var student = await _studentRepository.GetById(id);
                var result = new
                {
                    student.Id,
                    student.Name,
                    student.Phone,
                    student.Email,
                    student.FacultyId
                };
                return Ok(result);
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
                if (!ModelState.IsValid) { return BadRequest(ModelState); }

                var studentDto = new StudentDto()
                {
                    Id = studentVM.Id,
                    Name = studentVM.Name,
                    Phone = studentVM.Phone,
                    Email = studentVM.Email,
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
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                var studentDto = new StudentDto()
                {
                    Id = studentVM.Id,
                    Name = studentVM.Name,
                    Phone = studentVM.Phone,
                    Email = studentVM.Email,
                    FacultyId = studentVM.FacultyId
                };
                await _studentService.UpdateStudentAsync(id, studentDto);
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
