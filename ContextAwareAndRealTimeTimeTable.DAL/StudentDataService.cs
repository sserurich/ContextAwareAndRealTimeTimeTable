using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;
using Md = ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class StudentDataService:DataServiceBase, IStudentDataService
    {
         public StudentDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public IEnumerable<Student> GetAllStudents()
         {
             return this.UnitOfWork.Get<Student>().AsQueryable()
                    .Where(y => y.DeletedOn == null);
         }

         public int SaveStudent(Md.Student student)
         {
             if (student.StudentId == 0)
             {
                 var c = new Student()
                 {
                     StudentNumber = student.StudentNumber,
                     RegistrationNumber = student.RegistrationNumber,
                     UserId = student.UserId,
                     CreatedOn = DateTime.Now,

                 };
                 this.UnitOfWork.Get<Student>().AddNew(c);
                 this.UnitOfWork.SaveChanges();
                 return c.StudentId;
             }
             else
             {
                 var result = this.UnitOfWork.Get<Student>()
                     .AsQueryable().FirstOrDefault(s => s.StudentId == student.StudentId);
                 if (result != null)
                 {
                     result.StudentNumber = student.StudentNumber;
                     result.RegistrationNumber = student.RegistrationNumber;                    
                     result.UpdatedOn = DateTime.Now;
                     this.UnitOfWork.Get<Student>().Update(result);
                     this.UnitOfWork.SaveChanges();
                 }
                 return student.StudentId;
             }
         }

         public int GetStudentId(string userId)
         {
             int studentId = 0;
              var result = this.UnitOfWork.Get<Student>()
                     .AsQueryable().FirstOrDefault(s => s.UserId== userId);
              if (result != null)
              {
                  studentId = result.StudentId;
              }
              return studentId;
         }

         public int GetStudentsId(Models.Student student)
         {
             int studentId = 0;
             var result = this.UnitOfWork.Get<Student>()
                    .AsQueryable().FirstOrDefault(s => s.RegistrationNumber == student.RegistrationNumber && s.StudentNumber== student.StudentNumber);
             if (result != null)
             {
                 studentId = result.StudentId;
             }
             return studentId;
         }

         public List<string> GetUserIds(List<int> studentIds)
         {
             List<string> userIds = new List<string>();
             foreach (int studentId in studentIds)
             {
                 var result = this.UnitOfWork.Get<Student>()
                     .AsQueryable().FirstOrDefault(s => s.StudentId == studentId);
                 if (result != null)
                 {
                     userIds.Add(result.UserId);
                 }
             }
             return userIds;
            
         }

         public List<AspNetUser> GetAllUsersWithTheSpecifiedUserIds(List<string> userIds)
         {
             List<AspNetUser> users = new List<AspNetUser>();
             foreach (string userId in userIds)
             {
                 var result = this.UnitOfWork.Get<AspNetUser>()
                   .AsQueryable().FirstOrDefault(s => s.Id == userId);
                 if (result != null)
                 {
                     users.Add(result);
                 }
             }
             return users;
         }
    }
}
