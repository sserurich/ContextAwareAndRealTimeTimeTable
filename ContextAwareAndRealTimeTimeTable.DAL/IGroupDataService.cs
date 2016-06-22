using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.Models;
using Md = ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public interface IGroupDataService
    {
        IEnumerable<EF.Models.Group> GetAllGroups();

       IEnumerable<Group> GetGroupsForSpecifiedYearsAndCourses(Md.YearCourseGroup input);

       Group GetGroup(int groupId);

       
    }
}
