using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.DAL;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface IStudentYearService
    {
        IEnumerable<StudentYear> GetAllStudentSelectedYears(int studentId);
        int SaveStudentYear(StudentYear studentYear);
    }
}
