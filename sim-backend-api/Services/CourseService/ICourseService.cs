using sim_backend_api.DTOs.Course;

namespace sim_backend_api.Services.Course
{
    public interface ICourseService
    {
        Task<CourseDto> CreateAsync(CreateCourseDto dto,CancellationToken ct);

        Task<List<CourseDto>> GetAllAsync(string? search,CancellationToken ct);

        Task<CourseDto?> GetByIdAsync(int id,CancellationToken ct);

        Task<bool> UpdateAsync(int id,UpdateCourseDto dto,CancellationToken ct);

        Task<bool> DeleteAsync(int id,CancellationToken ct);
    }
}
