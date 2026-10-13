using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace sim_backend_api.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        public string FirstName { get; set; } = default!;

        [Required]
        public string LastName { get; set; } = default!;

        public int Gender { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        public int CourseId { get; set; } = new();
        public Course Course { get; set; } = null!;

        [Required, Phone]
        public string PhoneNumber { get; set; } = default!;

        [DataType(DataType.Date)]
        public DateOnly EnrollmentDate { get; set; }
        public string DocumentUrl { get; set; } = default!;
    }
}
