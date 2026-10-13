namespace sim_backend_api.DTOs.Student
{
    public class StudentResponseDto
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CourseName { get; set; } = string.Empty;
    }
}
