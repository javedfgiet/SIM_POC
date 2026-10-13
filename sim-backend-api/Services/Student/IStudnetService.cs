using sim_backend_api.DTOs.Student;
using sim_backend_api.Models;

namespace sim_backend_api.Services.Student
{
    public interface IStudnetService
    {
        Task<List<StudentResponseDto>> GetAllAsync(string? search, CancellationToken ct);

        Task<StudentResponseDto?> GetStudentByIdAsync(int Id, CancellationToken ct);
        Task<StudentResponseDto> CreateAsync(CreateStudentDto dto, CancellationToken ct);
        Task<bool> UpdateAsync(int Id, UpdateStudentDto dto, CancellationToken ct);

        Task<bool> DeleteAsync(int id, CancellationToken ct);
    }
}
