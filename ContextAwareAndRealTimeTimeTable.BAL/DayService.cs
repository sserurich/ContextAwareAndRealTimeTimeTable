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
    public class DayService: IDayService
    {
        private IDayDataService _dataService;
        public DayService( DayDataService dataService)
        {
            this._dataService = dataService;
        }

        public IEnumerable<Day> GetAllDays()
        {
            var results = this._dataService.GetAllDays();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Day, ContextAwareAndRealTimeTimeTable.Models.Day>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.Day>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.Day>>(results);
        }
    }
}
