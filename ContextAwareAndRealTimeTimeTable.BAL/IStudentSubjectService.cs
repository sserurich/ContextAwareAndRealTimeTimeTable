using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface IStudentSubjectService
    {
        IEnumerable<StudentSubject> GetAllStudentSubjects(int subjectId);
        int SaveStudentSubject(StudentSubject studentSubject);
        List<int> GetAllStudentIdsAttachedToASubject(int subjectId);
       
    }
}
