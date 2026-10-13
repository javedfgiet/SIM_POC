using Microsoft.EntityFrameworkCore;
using sim_backend_api.Data;
using sim_backend_api.DTOs.Course;
using sim_backend_api.Models;

namespace sim_backend_api.Services.Course
{
    public class CourseService(AppDbContext _context) : ICourseService
    {
        public async Task<CourseDto> CreateAsync(CreateCourseDto dto, CancellationToken ct)
        {
           
            var course = new Models.Course
            {
                CourseName = dto.CourseName
            };
            await _context.Courses.AddAsync(course, ct);
            await _context.SaveChangesAsync(ct);
            return new CourseDto
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                StudentCount = 0
            };
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id, ct);
            if (course==null)
                return false;
            
                
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CourseDto>> GetAllAsync(string? search, CancellationToken ct)
        {
            IQueryable<Models.Course> query = _context.Courses.AsNoTracking().Include(c => c.Studnets);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query=query.Where(c=>c.CourseName.Contains(search));
            }
            return await query.Select(c => new CourseDto
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                StudentCount = c.Studnets.Count
            }).ToListAsync(ct);
        }

        public async Task<CourseDto?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Courses.AsNoTracking()
                 .Include(c => c.Studnets)
                 .Where(c => c.CourseId == id)
                 .Select(c => new CourseDto
                 {
                     CourseId = c.CourseId,
                     CourseName = c.CourseName,
                     StudentCount = c.Studnets.Count

                 }).FirstOrDefaultAsync(ct);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCourseDto dto, CancellationToken ct)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == id, ct);
            if (course == null)
                return false;
            course.CourseName=dto.CourseName;
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }
}
