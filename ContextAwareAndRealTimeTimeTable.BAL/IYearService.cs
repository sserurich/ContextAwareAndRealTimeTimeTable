using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface IYearService
    {
        IEnumerable<Year> GetAllYears();
        List<Year> GetAllStudentSelectedYears(List<StudentYear> studentYears);
    }
}
