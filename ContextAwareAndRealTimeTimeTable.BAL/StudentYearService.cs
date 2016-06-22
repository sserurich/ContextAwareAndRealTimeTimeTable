using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ContextAwareAndRealTimeTimeTable.DAL;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public class StudentYearService : IStudentYearService{
   
        private IStudentYearDataService _dataService;
        public StudentYearService(StudentYearDataService dataService)
        {
            this._dataService = dataService;
        }

        public IEnumerable<StudentYear> GetAllStudentSelectedYears(int studentId)
        {
            var results = this._dataService.GetAllStudentSelectedYears(studentId);
            //Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.StudentYear, ContextAwareAndRealTimeTimeTable.Models.StudentYear>();
            //return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.StudentYear>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.StudentYear>>(results);
            var studentYearList = MapEfStudentYearObjectsToModelsStudentYearObject(results.ToList());
            return studentYearList;
        }

        public int SaveStudentYear(StudentYear studentYear)
        {
            return this._dataService.SaveStudentYear(studentYear);
        }

        public IEnumerable<StudentYear> MapEfStudentYearObjectsToModelsStudentYearObject(List<EF.Models.StudentYear> studentYearList)
        {
            var modelsStudentYearList = new List<StudentYear>();
            foreach (EF.Models.StudentYear studentYear in studentYearList)
            {

                modelsStudentYearList.Add(new ContextAwareAndRealTimeTimeTable.Models.StudentYear()
                {
                    StudentYearId = studentYear.StudentYearId,
                    StudentId = studentYear.StudentId,
                    YearId = studentYear.YearId,
                });
            }

            return modelsStudentYearList;
        }
    }
}
