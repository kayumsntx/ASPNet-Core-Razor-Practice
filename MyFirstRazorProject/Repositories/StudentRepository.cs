using Microsoft.EntityFrameworkCore;
using MyFirstRazorProject.Models;

namespace MyFirstRazorProject.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        
            private readonly AppDbContext _context;

            public StudentRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task AddModuleByStudentIdAsync(int studentId, List<CourseModule> courseModules)
            {
                if (courseModules != null || courseModules.Count > 0)
                {
                    foreach (var module in courseModules)
                    {
                        module.StudentId = studentId;
                        await _context.CourseModules.AddAsync(module);
                    }
                }
                await _context.SaveChangesAsync();
            }

            public async Task DeleteModuleByStudentIdAsync(int studentId)
            {
                var modules = await _context.CourseModules.Where(m => m.StudentId == studentId).ToListAsync();
                if (modules != null && modules.Any())
                {
                    _context.CourseModules.RemoveRange(modules);
                    _context.SaveChanges();
                }
            }

            public async Task<Student> DeleteStudentAsync(int id)
            {
                Student student = await _context.Students.FindAsync(id);
                if (student != null)
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                }
                return student;
            }

            public async Task<IEnumerable<Course>> GetAllCoursesAsync()
            {
                var courses = await _context.Courses.ToListAsync();
                return courses;
            }

            public async Task<IEnumerable<Student>> GetAllStudentAsync()
            {
                var students = await _context.Students.Include(c => c.Course).Include(m => m.CourseModules).ToListAsync();
                return students;
            }

            public async Task<IEnumerable<CourseModule>> GetCourseModulesByStudentId(int studentId)
            {
                var modules = await _context.CourseModules.Where(m => m.StudentId == studentId).ToListAsync();
                return modules;
            }

            public async Task<Student> GetStudentByIdAsync(int id)
            {
                var student = await _context.Students.Include(c => c.Course).Include(m => m.CourseModules).FirstOrDefaultAsync(s => s.StudentId == id);
                return student;
            }

            public async Task<Student> SaveStudentAsync(Student student)
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return student;
            }

            public async Task<Student> UpdateStudentAsync(Student student)
            {
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return student;
            }
        }
    }

