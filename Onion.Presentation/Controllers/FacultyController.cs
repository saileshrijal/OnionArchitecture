using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Application.Dtos;
using Onion.Application.Repository.Interface;
using Onion.Application.Services.Interface;
using Onion.Presentation.ViewModels;

namespace Onion.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        private readonly IFacultyRepository _facultyRepository;

        public FacultyController(IFacultyService facultyService, IFacultyRepository facultyRepository)
        {
            _facultyService= facultyService;
            _facultyRepository= facultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var faculties = await _facultyRepository.GetAll();
                var result = faculties.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
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
                var faculty = await _facultyRepository.GetById(id);
                var result = new
                {
                    Id = faculty.Id,
                    Name = faculty.Name,
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FacultyVM facultyVM)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                var facultyDto = new FacultyDto()
                {
                    Name = facultyVM.Name,
                };
                await _facultyService.CreateFacultyAsync(facultyDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] FacultyVM facultyVM)
        {
            try
            {
                if (!ModelState.IsValid) { return BadRequest(ModelState); }
                var facultyDto = new FacultyDto()
                {
                    Name = facultyVM.Name
                };
                await _facultyService.UpdateFacultyAsync(id, facultyDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _facultyService.DeleteFacultyAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
