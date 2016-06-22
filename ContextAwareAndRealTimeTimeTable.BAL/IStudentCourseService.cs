using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;
namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface IStudentCourseService
    {
        IEnumerable<StudentCourse> GetAllStudentSelectedCourses(int studentId);
        int SaveStudentCourse(StudentCourse studentCourse);
    }
}
