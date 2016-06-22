using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;
using ContextAwareAndRealTimeTimeTable.DAL;
using AutoMapper;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public class StudentService: IStudentService
    {
        private IStudentDataService _dataService;
        public StudentService( StudentDataService dataService)
        {
            this._dataService = dataService;
        }

        public IEnumerable<Student> GetAllStudents()
        {
            var results = this._dataService.GetAllStudents();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Student, ContextAwareAndRealTimeTimeTable.Models.Student>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.Student>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.Student>>(results);
        }

        public int SaveStudent(Student student)
        {
            return this._dataService.SaveStudent(student);
        }

        public int GetStudentId(string userId)
        {
            return this._dataService.GetStudentId(userId);
        }

        public int GetStudentsId(Student student)
        {
            return this._dataService.GetStudentsId(student);
        }

        public List<string> GetUserIds(List<int> studentids)
        {
            return this._dataService.GetUserIds(studentids);
        }

        public List<AspNetUser> GetAllUsersWithTheSpecifiedUserIds(List<string> userIds)
        {
            List<AspNetUser> usersToBeNotified = new List<AspNetUser>();
            var results= this._dataService.GetAllUsersWithTheSpecifiedUserIds(userIds);
            if (results != null)
            {
                foreach (var result in results)
                {
                    usersToBeNotified.Add(new AspNetUser()
                    {
                        UserName = result.UserName,
                        Id = result.Id,
                        Mobile = result.Mobile,
                        PhoneNumber = result.PhoneNumber,
                        Email = result.Email,
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                    });
                }
            }
            return usersToBeNotified;
            
        }
    }
}
