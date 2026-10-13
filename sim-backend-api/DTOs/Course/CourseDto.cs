namespace sim_backend_api.DTOs.Course
{
    public class CourseDto
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; } = default!;

        public int StudentCount { get; set; }
    }
}
