using Microsoft.EntityFrameworkCore;
using sim_backend_api.Data;
using sim_backend_api.DTOs.Student;
using sim_backend_api.Models;
using System.Linq;

namespace sim_backend_api.Services.Student
{
    public class StudnetService(AppDbContext _context) : IStudnetService
    {
        public async Task<StudentResponseDto> CreateAsync(CreateStudentDto dto, CancellationToken ct)
        {

            var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == dto.CourseId, ct);

            if (!courseExists)
            {
                throw new Exception("Invalid Course Id");
            }

            var student = new Models.Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                Email = dto.Email,
                CourseId = dto.CourseId,
                PhoneNumber = dto.PhoneNumber,
                DocumentUrl = dto.DocumentUrl,
                EnrollmentDate = DateOnly.FromDateTime(DateTime.Now)
            };
            await _context.Students.AddAsync(student, ct);

            await _context.SaveChangesAsync(ct);

            return new StudentResponseDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                CourseName = student.Course.CourseName
            };
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.StudentId == id, ct);

            if (student is null)
                return false;

            _context.Students.Remove(student);

            await _context.SaveChangesAsync(ct);

            return true;
        }

        public async Task<List<StudentResponseDto>> GetAllAsync(string? search, CancellationToken ct)
        {
            IQueryable<Models.Student> query = _context.Students.AsNoTracking()
                .Include(s => s.Course);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.FirstName.Contains(search) || s.LastName.Contains(search));
            }

            return await query
                .Select(s => new StudentResponseDto
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    CourseName = s.Course.CourseName
                })
                .ToListAsync(ct);
        }

        public async Task<StudentResponseDto?> GetStudentByIdAsync(int Id, CancellationToken ct)
        {
            var student = await _context.Students
                .AsNoTracking()
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.StudentId == Id, ct);
            return new StudentResponseDto
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                CourseName = student.Course.CourseName
            };
        }

        public async Task<bool> UpdateAsync(int Id, UpdateStudentDto dto, CancellationToken ct)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == Id, ct);

            if (student is null)
                return false;

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.Email = dto.Email;
            student.PhoneNumber = dto.PhoneNumber;
            student.Gender = dto.Gender;
            student.CourseId = dto.CourseId;
            student.DocumentUrl = dto.DocumentUrl;

            await _context.SaveChangesAsync(ct);

            return true;
        }
    }
}
