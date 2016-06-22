using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface IStudentGroupService
    {
        IEnumerable<StudentGroup> GetAllStudentGroups(int studentId);
        int SaveStudentGroup(StudentGroup studentGroup);
        List<int> GetAllStudentIdsAttachedToAGroup(int groupId);
    }
}
