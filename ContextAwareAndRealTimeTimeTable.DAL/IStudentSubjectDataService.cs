using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public interface IStudentSubjectDataService
    {
        IEnumerable<StudentSubject> GetAllStudentSubjects(int studentId);
        int SaveStudentSubject(Models.StudentSubject studentSubject);


        List<int> GetAllStudentIdsAttachedToASubject(int subjectId);
    }
}
