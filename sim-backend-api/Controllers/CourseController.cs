using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sim_backend_api.DTOs.Course;
using sim_backend_api.Services.Course;

namespace sim_backend_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(ICourseService _courseService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Courses(string? search, CancellationToken ct)
        {
            var result = await _courseService.GetAllAsync(search, ct);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> CourseById(int id, CancellationToken ct)
        {
            var result = await _courseService.GetByIdAsync(id, ct);
            
            if (result == null)
                return NotFound();
            
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto,CancellationToken ct)
        {
            var result=await _courseService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(CourseById), new {id=result.CourseId},result);
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,UpdateCourseDto dto,CancellationToken ct)
        {
            var updated = await _courseService
                .UpdateAsync(id, dto, ct);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id,CancellationToken ct)
        {
            var deleted = await _courseService
                .DeleteAsync(id, ct);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
