using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstRazorProject.Models.ViewModels;
using MyFirstRazorProject.Repositories;

namespace MyFirstRazorProject.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly IStudentRepository _repo;
        private readonly IWebHostEnvironment _env;

        [BindProperty]
        public StudentViewModel StudentViewModel { get; set; }
        [BindProperty]
        public IFormFile? ProfileFile { get; set; }

        [BindProperty]
        public string OldImageUrl { get; set; }
        public EditModel(IStudentRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }
       
        public async Task <IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var student = await _repo.GetStudentByIdAsync(id.Value);
            if(student ==null)
            {
                return NotFound();
            }
            StudentViewModel = new StudentViewModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                AdmissionDate = student.AdmissionDate,
                MobileNo = student.MobileNo,
                IsEnrolled = student.IsEnrolled,
                ImageUrl = student.ImageUrl,
                CourseId = student.CourseId,
                CourseFee = student.CourseFee,
                Courses = (await _repo.GetAllCoursesAsync()).ToList(),
                CourseModules =student.CourseModules.ToList(),

            };
            OldImageUrl = student.ImageUrl;
            return Page();
        }
    }
}
