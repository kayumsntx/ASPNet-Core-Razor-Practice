using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyFirstRazorProject.Models;
using MyFirstRazorProject.Repositories;

namespace MyFirstRazorProject.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly IStudentRepository _repo;
        public IEnumerable<Student> Students;

        public IndexModel(IStudentRepository repo, IEnumerable<Student> students)
        {
            _repo = repo;
            Students = students;
        }

        public async Task OnGetAsync()
        {
            Students = await _repo.GetAllStudentAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _repo.DeleteStudentAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
