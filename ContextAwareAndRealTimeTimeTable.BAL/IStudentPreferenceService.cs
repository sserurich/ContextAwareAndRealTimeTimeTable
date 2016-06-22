using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface IStudentPreferenceService
    {
         
        bool SaveStudentPreferences(StudentPreferences studentPreferences);

        StudentPreferences GetStudentPreferences(int studentId);
    }
}
