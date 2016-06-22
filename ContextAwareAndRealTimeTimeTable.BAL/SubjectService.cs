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
    public class SubjectService:ISubjectService
    {
        private ISubjectDataService _dataService;
        
        public SubjectService( SubjectDataService dataService)
        {
            this._dataService = dataService;
        }

        public List<Subject> GetAllStudentSelectedSubjects(List<StudentSubject> studentSubjects)
        {
            List<Subject> selectedSubjects = new List<Subject>();
            var allSubjects = GetAllSubjects();
            foreach (var x in studentSubjects)
            {
                var Subject = allSubjects.AsQueryable().Where(c => c.SubjectId == x.SubjectId).FirstOrDefault();
                if (Subject != null)
                {
                    selectedSubjects.Add(Subject);
                }
            }
            return selectedSubjects;

        }

        public IEnumerable<Subject> GetAllSubjects()
        {
            var results = this._dataService.GetAllSubjects();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Subject, ContextAwareAndRealTimeTimeTable.Models.Subject>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.Subject>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.Subject>>(results);
        }

        public IEnumerable<Subject> GetSubjectsForSpecifiedCoursesYearsAndGroups(CourseGroups input)
        {
            List<int> subjectIds = GetSubjectIds(input);
            var results = this._dataService.GetSubjectsForSpecifiedCoursesYearsAndGroups(subjectIds);
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Subject, ContextAwareAndRealTimeTimeTable.Models.Subject>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.Subject>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.Subject>>(results);

        }

        public List<int> GetSubjectIds(CourseGroups coursesAndGroups)
        {
            return this._dataService.GetSubjectIds(coursesAndGroups);
        }
    }
}
