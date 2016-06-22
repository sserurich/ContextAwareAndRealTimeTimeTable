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
    public class StudentSubjectService : IStudentSubjectService
    {
        private IStudentSubjectDataService _dataService;

        public StudentSubjectService( StudentSubjectDataService dataService)
        {
            this._dataService = dataService;
        }

        public IEnumerable<StudentSubject> GetAllStudentSubjects(int studentId)
        {
            var results = this._dataService.GetAllStudentSubjects(studentId);
            //Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.StudentSubject, ContextAwareAndRealTimeTimeTable.Models.StudentSubject>();
            //return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.StudentSubject>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.StudentSubject>>(results);
            var studentSubjectList = MapEfStudentSubjectObjectsToModelsStudentSubjectObject(results.ToList());
            return studentSubjectList;
        }

        public List<int> GetAllStudentIdsAttachedToASubject(int subjectId)
        {
            return this._dataService.GetAllStudentIdsAttachedToASubject(subjectId);
        }

        public int SaveStudentSubject(StudentSubject studentSubject)
        {
            return this._dataService.SaveStudentSubject(studentSubject);
        }

        public IEnumerable<StudentSubject> MapEfStudentSubjectObjectsToModelsStudentSubjectObject(List<EF.Models.StudentSubject> studentSubjectList)
        {
            var modelsStudentSubjectList = new List<StudentSubject>();
            foreach (EF.Models.StudentSubject studentSubject in studentSubjectList)
            {

                modelsStudentSubjectList.Add(new ContextAwareAndRealTimeTimeTable.Models.StudentSubject()
                {
                    StudentSubjectId = studentSubject.StudentSubjectId,
                    StudentId = studentSubject.StudentId,
                    SubjectId = studentSubject.SubjectId
                    
                });
            }

            return modelsStudentSubjectList;
        }
    }
}
