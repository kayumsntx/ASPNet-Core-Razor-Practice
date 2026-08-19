using MyFirstRazorProject.Models;

namespace MyFirstRazorProject.Repositories
{
    public interface IStudentRepository
    {
      
            Task<IEnumerable<Student>> GetAllStudentAsync();
            Task<Student> GetStudentByIdAsync(int id);
            Task<Student> SaveStudentAsync(Student student);
            Task<Student> UpdateStudentAsync(Student student);
            Task<IEnumerable<Course>> GetAllCoursesAsync();
            Task<Student> DeleteStudentAsync(int id);
            Task DeleteModuleByStudentIdAsync(int studentId);
            Task AddModuleByStudentIdAsync(int studentId, List<CourseModule> courseModules);
            Task<IEnumerable<CourseModule>> GetCourseModulesByStudentId(int studentId);
        }
    
}
