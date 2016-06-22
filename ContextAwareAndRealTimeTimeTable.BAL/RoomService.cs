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
    public class RoomService: IRoomService
    {
        private IRoomDataService _dataService;
        public RoomService( RoomDataService dataService)
        {
            this._dataService = dataService;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            var results = this._dataService.GetAllRooms();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Room, ContextAwareAndRealTimeTimeTable.Models.Room>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.Room>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.Room>>(results);
        }
    }
}
