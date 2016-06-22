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
    public class GroupActivityService: IGroupActivityService
    {
        private IGroupActivityDataService _dataService;
        public GroupActivityService( GroupActivityDataService dataService)
        {
            this._dataService = dataService;
        }

        public IEnumerable<GroupActivity> GetAllGroupActivities()
        {
            var results = this._dataService.GetAllGroupActivities();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.GroupActivity, ContextAwareAndRealTimeTimeTable.Models.GroupActivity>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.GroupActivity>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.GroupActivity>>(results);
        }
    }
}
