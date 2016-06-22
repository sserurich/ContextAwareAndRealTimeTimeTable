using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class CourseDataService:DataServiceBase, ICourseDataService
    {
         public CourseDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public IEnumerable<Course> GetAllCourses()
         {
             return this.UnitOfWork.Get<Course>().AsQueryable()
                    .Where(y => y.DeletedOn == null);
         }


    }
}
