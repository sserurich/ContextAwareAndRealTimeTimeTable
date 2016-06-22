using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.Models;
using ML = ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public interface IStudentCourseDataService
    {
        IEnumerable<StudentCourse> GetAllStudentSelectedCourses(int studentId);
        int SaveStudentCourse(ML.StudentCourse studentCourse);
    }
}
