using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class StudentSubjectDataService:DataServiceBase, IStudentSubjectDataService
    {
         public StudentSubjectDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public List<int> GetAllStudentIdsAttachedToASubject(int subjectId)
         {
             List<int> studentIds = new List<int>();
             var results = this.UnitOfWork.Get<StudentSubject>().AsQueryable()
                    .Where(y => y.SubjectId == subjectId);
             if (results != null)
             {
                 foreach (var studentSubject in results)
                 {
                     studentIds.Add(studentSubject.StudentId);
                 }
             }

             return studentIds;
         }

         public IEnumerable<StudentSubject> GetAllStudentSubjects(int studentId)
         {
             return this.UnitOfWork.Get<StudentSubject>().AsQueryable()
                    .Where(y => y.DeletedOn == null&& y.StudentId == studentId);
         }

         public int SaveStudentSubject(Models.StudentSubject studentSubject)
         {
             if (studentSubject.StudentSubjectId == 0)
             {
                 var studentSubjectToBeSaved = new EF.Models.StudentSubject()
                 {
                     StudentId = studentSubject.StudentId,
                     SubjectId = studentSubject.SubjectId,
                     CreatedOn = DateTime.Now
                 };
                 var x = this.UnitOfWork.Get<StudentSubject>().AddNew(studentSubjectToBeSaved);
                 this.UnitOfWork.SaveChanges();
                 return x.StudentSubjectId;
             }
             else
             {
                 var result = this.UnitOfWork.Get<StudentSubject>().AsQueryable().Where(a => a.StudentSubjectId == studentSubject.StudentSubjectId).FirstOrDefault();
                 if (result != null)
                 {
                     result.SubjectId = studentSubject.SubjectId;
                     result.StudentId = studentSubject.StudentId;
                     result.UpdatedOn = DateTime.Now;
                     this.UnitOfWork.Get<StudentSubject>().Update(result);
                     this.UnitOfWork.SaveChanges();
                 }
                 return studentSubject.StudentSubjectId;
             }
         }
    }
}
