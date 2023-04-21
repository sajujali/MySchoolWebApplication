using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    public class Student : EditImageViewModel
    {
        [Required]
        public int Id { get; set; }
        [StringLength(100, MinimumLength =4)]
        public string Name { get; set; }
        public string? Class { get; set; }
        [DefaultValue(1)]
        public int? GradeId { get; set; } = 1;

        /*[Required(ErrorMessage = "Please choose profile image")]
        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile? ProfileImage { get; set; }*/
    }

    public class UploadImageViewModel
    {
        [Required]
        [Display(Name = "Image")]
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
    }

    public class EditImageViewModel : UploadImageViewModel
    {
        public int Id { get; set; }
        public string? ProfileImageName { get; set; }
    }


}
