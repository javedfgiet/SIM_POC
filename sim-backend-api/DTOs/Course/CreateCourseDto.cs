using System.ComponentModel.DataAnnotations;

namespace sim_backend_api.DTOs.Course
{
    public class CreateCourseDto
    {
        [Required]
        public string CourseName { get; set; } = default!;
    }
}
