using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class GroupActivityDataService:DataServiceBase, IGroupActivityDataService
    {
         public GroupActivityDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public IEnumerable<GroupActivity> GetAllGroupActivities()
         {
             return this.UnitOfWork.Get<GroupActivity>().AsQueryable()
                    .Where(y => y.DeletedOn == null);
         }
    }
}
