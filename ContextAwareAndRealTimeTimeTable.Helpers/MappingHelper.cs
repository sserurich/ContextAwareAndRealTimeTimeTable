using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;
using ContextAwareAndRealTimeTimeTable.EF;

namespace ContextAwareAndRealTimeTimeTable.Helpers
{
    public class MappingHelper
    {
        public static Course MapEFCourseToModelCourse(EF.Models.Course course)
        {
            var foundCourse = new Models.Course();
            if (course != null)
            {
                foundCourse.Name = course.Name;
                foundCourse.CourseId = course.CourseId;
                foundCourse.Description = course.Description;
            }
            return foundCourse;
        }

        public static Year MapEFYearToModelYear(EF.Models.Year year)
        {
            var foundYear = new Models.Year();
            if (year != null)
            {
                foundYear.Name = year.Name;
                foundYear.YearId = year.YearId;
            }
            return foundYear;
        }




    }
}
