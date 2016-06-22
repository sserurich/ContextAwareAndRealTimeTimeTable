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
    public class StudentCourseService:IStudentCourseService
    {

         private IStudentCourseDataService _dataService;

        public StudentCourseService( StudentCourseDataService dataService)
        {
            this._dataService = dataService;
        }

        public IEnumerable<StudentCourse> GetAllStudentSelectedCourses(int studentId)
        {
            var results = this._dataService.GetAllStudentSelectedCourses(studentId);
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.StudentCourse, ContextAwareAndRealTimeTimeTable.Models.StudentCourse>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.StudentCourse>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.StudentCourse>>(results);
        }


        public int SaveStudentCourse(StudentCourse studentCourse)
        {
            return this._dataService.SaveStudentCourse(studentCourse);
        }

        public IEnumerable<StudentCourse> MapEfStudentCourseObjectsToModelsStudentCourseObject(List<EF.Models.StudentCourse> studentCourseList)
        {
            var modelsStudentCourseList = new List<StudentCourse>();
            foreach (EF.Models.StudentCourse studentCourse in studentCourseList)
            {

                modelsStudentCourseList.Add(new ContextAwareAndRealTimeTimeTable.Models.StudentCourse()
                {
                    StudentCourseId = studentCourse.StudentCourseId,
                    StudentId = studentCourse.StudentId,
                    CourseId = studentCourse.CourseId

                });
            }

            return modelsStudentCourseList;
        }

    }
}
