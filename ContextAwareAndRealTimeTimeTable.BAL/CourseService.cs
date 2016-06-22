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
    public class CourseService : ICourseService
    {
        private ICourseDataService _dataService;
        public CourseService( CourseDataService dataService)
        {
            this._dataService = dataService;
        }

        public List<Course> GetAllStudentSelectedCourses(List<StudentCourse> studentCourses)
        {
            List<Course> selectedCourses = new List<Course>();
            var allCourses = GetAllCourses();
            foreach (var x in studentCourses)
            {
                var course = allCourses.AsQueryable().
                    Where(c => c.CourseId == x.CourseId)
                    .FirstOrDefault();
                if (course != null)
                {
                    selectedCourses.Add(course);
                }
            }
            return selectedCourses;

        }

        public IEnumerable<Course> GetAllCourses()
        {
            var results = this._dataService.GetAllCourses();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Course, ContextAwareAndRealTimeTimeTable.Models.Course>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.Course>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.Course>>(results);
        }
    }
}
