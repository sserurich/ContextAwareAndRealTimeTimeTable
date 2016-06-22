using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface IGroupService
    {
        IEnumerable<Group> GetAllGroups();

        IEnumerable<Group> GetGroupsForSpecifiedYearsAndCourses(YearCourseGroup input);

        Group GetGroup(int groupId);

        List<Group> GetAllStudentSelectedGroups(List<StudentGroup> studentGroups);
    }
}
