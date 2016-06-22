using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.Models;
using ML= ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public interface IStudentYearDataService
    {
        IEnumerable<StudentYear> GetAllStudentSelectedYears(int studentId);
        int SaveStudentYear(ML.StudentYear studentYear);
    }
}
