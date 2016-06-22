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
    public class StudentYearDataService:DataServiceBase, IStudentYearDataService
    {
         public StudentYearDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {
             
        }

        public IEnumerable<StudentYear> GetAllStudentSelectedYears(int studentId)
         {
             return this.UnitOfWork.Get<StudentYear>().AsQueryable()
                    .Where(y => y.DeletedOn == null && y.StudentId==studentId);
         }

        public int SaveStudentYear(ML.StudentYear studentYear)
         {
             if (studentYear.StudentYearId == 0)
             {
                 var studentYearToBeSaved = new EF.Models.StudentYear()
                 {
                     StudentId = studentYear.StudentId,
                     YearId = studentYear.YearId,
                     CreatedOn = DateTime.Now                     
                 };
                var x =  this.UnitOfWork.Get<StudentYear>().AddNew(studentYearToBeSaved);
                 this.UnitOfWork.SaveChanges();
                 return x.StudentYearId;
             }
             else
             {
                 var result = this.UnitOfWork.Get<StudentYear>().AsQueryable().Where(a => a.StudentYearId == studentYear.StudentYearId).FirstOrDefault();
                 if (result != null)
                 {
                     result.YearId = studentYear.YearId;
                     result.StudentId = studentYear.StudentId;
                     result.UpdatedOn = DateTime.Now;
                     this.UnitOfWork.Get<StudentYear>().Update(result);
                     this.UnitOfWork.SaveChanges();                    
                 }
                 return studentYear.StudentYearId;
             }
         }
    }
}
