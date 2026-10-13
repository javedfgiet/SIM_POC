using sim_backend_api.Models;
using System.ComponentModel.DataAnnotations;

namespace sim_backend_api.DTOs.Student
{
    public class CreateStudentDto
    {
        public string FirstName { get; set; } = default!;

        [Required]
        public string LastName { get; set; } = default!;

        public int Gender { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = default!;
        public int CourseId { get; set; } = new();     

        [Required, Phone]
        public string PhoneNumber { get; set; } = default!;

        public string DocumentUrl { get; set; } = default!;
    }
}
