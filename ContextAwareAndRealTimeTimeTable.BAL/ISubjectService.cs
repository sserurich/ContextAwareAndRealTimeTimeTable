using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface ISubjectService
    {
        IEnumerable<Subject> GetAllSubjects();

        IEnumerable<Subject> GetSubjectsForSpecifiedCoursesYearsAndGroups(CourseGroups input);

        List<Subject> GetAllStudentSelectedSubjects(List<StudentSubject> studentSubjects);


    }
}
