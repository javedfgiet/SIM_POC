using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sim_backend_api.DTOs.Student;
using sim_backend_api.Services.Student;

namespace sim_backend_api.Controllers
{
    [Authorize(Roles ="User")]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController(IStudnetService _studentService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Students(string? search, CancellationToken ct)
        {
            var students = await _studentService.GetAllAsync(search, ct);
            return Ok(students);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> StudentById(int Id, CancellationToken ct)
        {
            var student = await _studentService.GetStudentByIdAsync(Id, ct);
            if (student is null)
            {
                return NotFound(new { message = "Student not Found" });
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto, CancellationToken ct)
        {
            var student = await _studentService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(StudentById), new { Id = student.StudentId }, student);
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateStudentDto dto, CancellationToken ct)
        {
            var updated = await _studentService.UpdateAsync(id, dto, ct);

            if (!updated)
                return NotFound(new { message = "Student Not Found" });

            return NoContent();
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id, CancellationToken ct)
        {
            var deleted = await _studentService.DeleteAsync(Id, ct);
            if (!deleted)
                return NotFound(new { message = "Student Not Found" });
            return NoContent();
        }

    }
}
