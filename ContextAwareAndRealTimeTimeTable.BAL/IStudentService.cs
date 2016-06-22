using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents();
        int SaveStudent(Student student);

        int GetStudentId(string userId);

        List<string> GetUserIds(List<int> studentids);

        List<AspNetUser> GetAllUsersWithTheSpecifiedUserIds(List<string> userIds);

        int GetStudentsId(Student student);

        
    }
}
