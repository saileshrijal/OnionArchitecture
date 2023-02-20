using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Application.Dtos;
using Onion.Application.Services.Interface;
using Onion.Presentation.ViewModels;

namespace Onion.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService= facultyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var facultiesDto = await _facultyService.GetAllFacultiesAsync();
                var facultiesVM = facultiesDto.Select(x => new FacultyVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                });
                return Ok(facultiesVM);
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
                var facultyDto = await _facultyService.GetFacultyByIdAsync(id);
                var facultyVM = new FacultyVM()
                {
                    Id = facultyDto.Id,
                    Name = facultyDto.Name,
                };
                return Ok(facultyVM);
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
                var facultyDto = new FacultyDto()
                {
                    Name = facultyVM.Name,
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
