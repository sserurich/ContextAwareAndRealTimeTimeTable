using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.Models;


namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public interface ISubjectDataService
    {
        IEnumerable<Subject> GetAllSubjects();

        List<int> GetSubjectIds(Models.CourseGroups coursesAndGroups);

        IEnumerable<Subject> GetSubjectsForSpecifiedCoursesYearsAndGroups(List<int> subjectIds);
    }
}
