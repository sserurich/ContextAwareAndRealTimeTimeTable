using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class RoomDataService:DataServiceBase, IRoomDataService
    {
         public RoomDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public IEnumerable<Room> GetAllRooms()
         {
             return this.UnitOfWork.Get<Room>().AsQueryable()
                    .Where(y => y.DeletedOn == null);
         }
    }
}
