using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class SubjectDataService:DataServiceBase, ISubjectDataService
    {
        public SubjectDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }


         public IEnumerable<Subject> GetAllSubjects()
         {
             return this.UnitOfWork.Get<Subject>().AsQueryable()
                    .Where(y => y.DeletedOn == null);
         }

         public IEnumerable<Subject> GetSubjectsForSpecifiedCoursesYearsAndGroups(List<int> subjectIds)
         {
             var subjects = new List<Subject>();
             if(subjectIds.Count>0){
                 foreach (int subjectId in subjectIds)
                 {
                     var subject = this.UnitOfWork.Get<Subject>()
                         .AsQueryable().Where(s => s.DeletedOn == null && s.SubjectId == subjectId)
                         .FirstOrDefault();
                     if (subject != null)
                     {
                         subjects.Add(subject);
                     }                    
                 }
             }
            return subjects;
             
         }


         public List<int> GetSubjectIds(Models.CourseGroups coursesAndGroups)
         {
             int groupId, courseId, yearId;
             var subjectIds = new List<int>();
             var subjectCourseIds = new List<int>();
             for (int i = 0; i < coursesAndGroups.Groups.Count; i++)
             {
                 groupId = coursesAndGroups.Groups[i].GroupId;
                 for (int j = 0; j < coursesAndGroups.Courses.Count; j++)
                 {
                     courseId = coursesAndGroups.Courses[j].CourseId;
                     var result = this.UnitOfWork.Get<CourseGroupSubject>().AsQueryable()
                         .Where(y => y.DeletedOn == null && (y.GroupId == groupId && y.CourseId == courseId));
                     if (result != null)
                     {
                         foreach (var g in result.Distinct())
                         {
                             subjectIds.Add(g.SubjectId);
                         }
                     }
                 }
             }

             foreach(var g in subjectIds)
             {
            
             for (int x = 0; x < coursesAndGroups.Years.Count; x++)
             {
                 yearId = coursesAndGroups.Years[x].YearId;
                 var res = this.UnitOfWork.Get<SubjectYear>().AsQueryable()
                     .Where(y => y.DeletedOn == null && (y.SubjectId == g) && (y.YearId == yearId));
                 if (res != null)
                 {
                     foreach (var w in res.Distinct())
                     {
                         subjectCourseIds.Add(w.SubjectId);
                     }
                 }
             }

             }
                 return subjectCourseIds;
         }
    }
}
