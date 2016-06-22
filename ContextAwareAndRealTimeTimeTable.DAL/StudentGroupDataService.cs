using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class StudentGroupDataService:DataServiceBase, IStudentGroupDataService
    {
         public StudentGroupDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public List<int> GetAllStudentIdsAttachedToAGroup(int groupId)
         {
             List<int> studentIds = new List<int>();
             var results = this.UnitOfWork.Get<StudentGroup>().AsQueryable()
                    .Where(y =>y.GroupId == groupId);
             if (results != null)
             {
                 foreach (var studentGroup in results)
                 {
                     studentIds.Add(studentGroup.StudentId);
                 }
             }

             return studentIds;
         }

         public IEnumerable<StudentGroup> GetAllStudentGroups(int studentId)
         {
             return this.UnitOfWork.Get<StudentGroup>().AsQueryable()
                    .Where(y => y.DeletedOn == null && y.StudentId== studentId);
         }

         public int SaveStudentGroup(Models.StudentGroup studentGroup)
         {
             if (studentGroup.StudentGroupId == 0)
             {
                 
                     var c = new StudentGroup()
                     {
                         GroupId = studentGroup.GroupId,
                         StudentId = studentGroup.StudentId,
                         CreatedOn = DateTime.Now
                     };
                     this.UnitOfWork.Get<StudentGroup>().AddNew(c);
                     this.UnitOfWork.SaveChanges();
                     return c.StudentGroupId;                 
                
             }
             else
             {
                 var c = this.UnitOfWork.Get<StudentGroup>().AsQueryable()
                         .FirstOrDefault(d => d.StudentGroupId == studentGroup.StudentGroupId);
                 if (c != null)
                 {
                        c. GroupId = studentGroup.GroupId;
                        c.StudentId = studentGroup.StudentId;                     
                       c.UpdatedOn= DateTime.Now;
                     this.UnitOfWork.Get<StudentGroup>().Update(c);
                     this.UnitOfWork.SaveChanges();
                 }
                 return studentGroup.StudentGroupId;
             }
         }
    }
}
