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
    public class YearService: IYearService
    {
        private IYearDataService _dataService;
        public YearService( YearDataService dataService)
        {
            this._dataService = dataService;
        }

        public List<Year> GetAllStudentSelectedYears(List<StudentYear> studentYears)
        {
            List<Year> selectedYears = new List<Year>();
            var allYears = GetAllYears();
            foreach (var x in studentYears)
            {
                var year = allYears.AsQueryable().Where(c => c.YearId == x.YearId).FirstOrDefault();
                if (year != null)
                {
                    selectedYears.Add(year);
                }
            }
            return selectedYears;

        }


        public IEnumerable<Year> GetAllYears()
        {
            var results = this._dataService.GetAllYears();
            var yearList = MapEfYearObjectsToModelsYearObject(results.ToList());
            return yearList;
        }



        public IEnumerable<Year> MapEfYearObjectsToModelsYearObject(List<EF.Models.Year> YearList)
        {
            var modelsYearList = new List<Year>();
            foreach (EF.Models.Year Year in YearList)
            {
                                
                modelsYearList.Add(new ContextAwareAndRealTimeTimeTable.Models.Year()
                {
                    YearId = Year.YearId,
                    Name = Year.Name                   
                });
            }

            return modelsYearList;
        }
    }
}
