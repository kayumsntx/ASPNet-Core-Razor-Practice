using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyFirstRazorProject.Models.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }
        [Display(Name = "Student Name")]
        public string StudentName { get; set; } = null!;
        [Display(Name = "Admission Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AdmissionDate { get; set; } = DateTime.Now;
        public string MobileNo { get; set; } = null!;
        [ValidateNever]
        public string ImageUrl { get; set; } = null!;
        public bool IsEnrolled { get; set; }
        public decimal CourseFee { get; set; }
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        [ValidateNever]
        public virtual Course Course { get; set; } = null!;
        [ValidateNever]
        public List<Course>? Courses { get; set; }
        [ValidateNever]
        public IFormFile? ProfileFile { get; set; }
        public IList<CourseModule> CourseModules { get; set; } = new List<CourseModule>();
    }
}
