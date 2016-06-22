using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using ContextAwareAndRealTimeTimeTable.BAL;
using ContextAwareAndRealTimeTimeTable.DAL;

namespace ContextAwareAndRealTimeTimeTable.DependencyResolver
{
    public class ServiceDependencyResolver :NinjectModule
    {
        public override void Load()
        {
          

            Bind(typeof(IStudentDataService)).To(typeof(StudentDataService));
            Bind(typeof(IStudentService)).To(typeof(StudentService));

            Bind(typeof(IRoomService)).To(typeof(RoomService));
            Bind(typeof(IRoomDataService)).To(typeof(RoomDataService));

            Bind(typeof(ILecturerService)).To(typeof(LecturerService));
            Bind(typeof(ILecturerDataService)).To(typeof(LecturerDataService));

            Bind(typeof(IDayService)).To(typeof(DayService));
            Bind(typeof(IDayDataService)).To(typeof(DayDataService));


            Bind(typeof(ISubjectService)).To(typeof(SubjectService));
            Bind(typeof(ISubjectDataService)).To(typeof(SubjectDataService));

            Bind(typeof(ICommentService)).To(typeof(CommentService));
            Bind(typeof(ICommentDataService)).To(typeof(CommentDataService));

            Bind(typeof(IGroupDataService)).To(typeof(GroupDataService));
            Bind(typeof(IGroupService)).To(typeof(GroupService));

            Bind(typeof(IYearService)).To(typeof(YearService));
            Bind(typeof(IYearDataService)).To(typeof(YearDataService));

            Bind(typeof(IActivityService)).To(typeof(ActivityService));
            Bind(typeof(IActivityDataService)).To(typeof(ActivityDataService));

            Bind(typeof(IStudentSubjectService)).To(typeof(StudentSubjectService));
            Bind(typeof(IStudentSubjectDataService)).To(typeof(StudentSubjectDataService));

            Bind(typeof(IStudentGroupService)).To(typeof(StudentGroupService));
            Bind(typeof(IStudentGroupDataService)).To(typeof(StudentGroupDataService));

            Bind(typeof(ICourseService)).To(typeof(CourseService));
            Bind(typeof(ICourseDataService)).To(typeof(CourseDataService));

            Bind(typeof(IStudentPreferenceService)).To(typeof(StudentPreferenceService));            

            Bind(typeof(IStudentYearService)).To(typeof(StudentYearService));
            Bind(typeof(IStudentYearDataService)).To(typeof(StudentYearDataService));

            Bind(typeof(IStudentCourseService)).To(typeof(StudentCourseService));
            Bind(typeof(IStudentCourseDataService)).To(typeof(StudentCourseDataService));



        }
    }
}
