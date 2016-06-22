using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class LecturerDataService :DataServiceBase, ILecturerDataService
    {
         public LecturerDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public IEnumerable<Lecturer> GetAllLecturers()
         {
             return this.UnitOfWork.Get<Lecturer>().AsQueryable()
                    .Where(y => y.DeletedOn == null);
         }
    }
}
