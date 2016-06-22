using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public interface IStudentDataService
    {
        IEnumerable<Student> GetAllStudents();

        int SaveStudent(Models.Student student);

        int GetStudentId(string userId);

        List<string> GetUserIds(List<int> studentids);

        List<AspNetUser> GetAllUsersWithTheSpecifiedUserIds(List<string> userIds);

        int GetStudentsId(Models.Student student);
    }
}
