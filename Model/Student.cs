using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [StringLength(100, MinimumLength =4)]
        public string Name { get; set; }
        public string? Class { get; set; }
        [DefaultValue(1)]
        public int? GradeId { get; set; } = 1;
    }
}
