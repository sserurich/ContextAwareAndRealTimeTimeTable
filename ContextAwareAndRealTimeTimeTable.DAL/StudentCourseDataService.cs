using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;
using ML = ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class StudentCourseDataService:DataServiceBase, IStudentCourseDataService
    {
         public StudentCourseDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {
             
        }

         public IEnumerable<StudentCourse> GetAllStudentSelectedCourses(int studentId)
         {
             return this.UnitOfWork.Get<StudentCourse>().AsQueryable()
                    .Where(y => y.DeletedOn == null && y.StudentId==studentId);
         }

        public int SaveStudentCourse(ML.StudentCourse studentCourse)
         {
             if (studentCourse.StudentCourseId == 0)
             {
                 var studentCourseToBeSaved = new EF.Models.StudentCourse()
                 {
                     StudentId = studentCourse.StudentId,
                     CourseId = studentCourse.CourseId,
                     CreatedOn = DateTime.Now                     
                 };
                var x =  this.UnitOfWork.Get<StudentCourse>().AddNew(studentCourseToBeSaved);
                 this.UnitOfWork.SaveChanges();
                 return x.StudentCourseId;
             }
             else
             {
                 var result = this.UnitOfWork.Get<StudentCourse>().AsQueryable().Where(a => a.StudentCourseId == studentCourse.StudentCourseId).FirstOrDefault();
                 if (result != null)
                 {
                     result.CourseId = studentCourse.CourseId;
                     result.StudentId = studentCourse.StudentId;
                     result.UpdatedOn = DateTime.Now;
                     this.UnitOfWork.Get<StudentCourse>().Update(result);
                     this.UnitOfWork.SaveChanges();                    
                 }
                 return studentCourse.StudentCourseId;
             }
         }
    }
}
