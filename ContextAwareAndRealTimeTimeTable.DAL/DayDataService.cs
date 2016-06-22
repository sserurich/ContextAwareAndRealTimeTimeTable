using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class DayDataService:DataServiceBase, IDayDataService
    {
         public DayDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public IEnumerable<Day> GetAllDays()
         {
             return this.UnitOfWork.Get<Day>().AsQueryable()
                    .Where(y => y.DeletedOn == null);
         }
    }
}
