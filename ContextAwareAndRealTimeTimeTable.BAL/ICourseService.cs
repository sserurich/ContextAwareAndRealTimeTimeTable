using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses();
        List<Course> GetAllStudentSelectedCourses(List<StudentCourse> studentCourses);
    }
}
