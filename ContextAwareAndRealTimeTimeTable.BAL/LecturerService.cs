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
    public class LecturerService : ILecturerService
    {
        private ILecturerDataService _dataService;
        public LecturerService( LecturerDataService dataService)
        {
            this._dataService = dataService;
        }

        public IEnumerable<Lecturer> GetAllLecturers()
        {
            var results = this._dataService.GetAllLecturers();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Lecturer, ContextAwareAndRealTimeTimeTable.Models.Lecturer>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.Lecturer>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.Lecturer>>(results);
        }
    }
}
