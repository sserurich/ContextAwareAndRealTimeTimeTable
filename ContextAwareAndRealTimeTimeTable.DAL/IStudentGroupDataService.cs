using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public interface IStudentGroupDataService
    {
        IEnumerable<StudentGroup> GetAllStudentGroups(int studentId);

        int SaveStudentGroup(Models.StudentGroup studentGroup);
        List<int> GetAllStudentIdsAttachedToAGroup(int groupId);
    }
}
