using System.ComponentModel.DataAnnotations;

namespace sim_backend_api.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required]
        public string CourseName { get; set; } = default!;
        public ICollection<Student> Studnets { get; set; } = new List<Student>();
    }
}
