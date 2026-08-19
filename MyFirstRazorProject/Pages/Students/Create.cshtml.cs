using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstRazorProject.Models;
using MyFirstRazorProject.Models.ViewModels;
using MyFirstRazorProject.Repositories;

namespace MyFirstRazorProject.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly IStudentRepository _repo;
        private readonly IWebHostEnvironment _env;

        public CreateModel(IStudentRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [BindProperty]
        public StudentViewModel StudentViewModel { get; set; }
        [BindProperty]
        public IFormFile? ProfileFile { get; set; }
        public IActionResult OnGet()
        {
            StudentViewModel = new StudentViewModel()
            {
                Courses = _repo.GetAllCoursesAsync().Result.ToList(),
            };
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                StudentViewModel.Courses = _repo.GetAllCoursesAsync().Result.ToList();

            }
            if (ProfileFile != null && ProfileFile.Length > 0)

            {
var fileName=Guid.NewGuid().ToString()+Path.GetExtension
                    (ProfileFile.FileName);
                var filepath = Path.Combine(_env.WebRootPath, "images", fileName);
                using (var fileStream=new FileStream(filepath,FileMode.Create))
                {
                    await ProfileFile.CopyToAsync(fileStream);
                    StudentViewModel.ImageUrl = fileName;

                }
            }
            else
            {
                StudentViewModel.ImageUrl = "images/noimage.png";
            }
            var student = new Student
            {
                StudentName = StudentViewModel.StudentName,
                AdmissionDate = StudentViewModel.AdmissionDate,
                MobileNo = StudentViewModel.MobileNo,
                IsEnrolled = StudentViewModel.IsEnrolled,
                CourseFee = StudentViewModel.CourseFee,
                CourseId = StudentViewModel.CourseId,
                CourseModules = StudentViewModel.CourseModules.ToList(),
                ImageUrl = StudentViewModel.ImageUrl,

            };
            await _repo.SaveStudentAsync(student);
            return RedirectToPage("./Index");
        }
    }
}
