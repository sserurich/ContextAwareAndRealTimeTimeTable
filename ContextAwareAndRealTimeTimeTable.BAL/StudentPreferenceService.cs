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
    public class StudentPreferenceService :IStudentPreferenceService
    {
        private IStudentGroupService _studentGroupService;
        private IStudentSubjectService _studentSubjectService;
        private IStudentCourseService _studentCourseService;
        private IStudentYearService _studentYearService;
        private ICourseService _courseService;
        private IYearService  _yearService;
        private ISubjectService _subjectService;
        private IGroupService _groupService;

        public StudentPreferenceService(StudentGroupService dataService, StudentSubjectService studentSubjectService, 
            StudentCourseService studentCourseService, StudentYearService studentYearService,CourseService courseService, YearService yearService,SubjectService subjectService,GroupService groupService)
        {
            this._studentGroupService = dataService;
            this._studentSubjectService = studentSubjectService;
            this._studentCourseService = studentCourseService;
            this._studentYearService = studentYearService;
            this._courseService = courseService;
            this._groupService = groupService;
            this._yearService = yearService;
            this._subjectService = subjectService;
        }

        public StudentPreferences GetStudentPreferences(int studentId)
        {
            StudentPreferences studentPreferences = new StudentPreferences();
            List<StudentCourse> studentSelectedCourses = _studentCourseService.GetAllStudentSelectedCourses(studentId).ToList();
            List<StudentYear> studentSelectedYears = _studentYearService.GetAllStudentSelectedYears(studentId).ToList();
            List<StudentSubject> studentSelectedSubjects = _studentSubjectService.GetAllStudentSubjects(studentId).ToList();
            List<StudentGroup> studentSelectedGroups = _studentGroupService.GetAllStudentGroups(studentId).ToList();
            studentPreferences.studentId = studentId;
            studentPreferences.SelectedCourses = _courseService.GetAllStudentSelectedCourses(studentSelectedCourses);
            studentPreferences.SelectedSubjects = _subjectService.GetAllStudentSelectedSubjects(studentSelectedSubjects);
            studentPreferences.SelectedYears = _yearService.GetAllStudentSelectedYears(studentSelectedYears);
            studentPreferences.SelectedGroups = _groupService.GetAllStudentSelectedGroups(studentSelectedGroups);

            return studentPreferences;          
        }

        public bool SaveStudentPreferences(StudentPreferences studentPreferences)
        {
            bool saved = true;
            Result courseResult = new Result()
                 {
                     Message = "",
                     Status = "Not Failed"
                 };
            Result subjectResult = new Result()
            {
                     Message = "",
                     Status = "Not Failed"
            };
             Result yearResult = new Result()
            {
                     Message = "",
                     Status = "Not Failed"
            };
             Result groupResult = new Result()
             {
                 Message = "",
                 Status = "Not Failed"
             };
            var selectedCourses = studentPreferences.SelectedCourses;          
            if (selectedCourses != null)
            {        
            foreach (var x in selectedCourses)
            {
                StudentCourse selectedCourse = new StudentCourse(){
                    CourseId= x.CourseId,
                    StudentId =studentPreferences.studentId,
                    CreatedOn = DateTime.Now
                };
             var returnedCourseId =   this._studentCourseService.SaveStudentCourse(selectedCourse);
             if (returnedCourseId == 0)
             {
                 courseResult = new Result()
                 {
                     Message = "could not save the course with Id" + x.CourseId,
                     Status = "Failed"
                 };
             }
            }

            }

            if (studentPreferences.SelectedSubjects != null)
            {
                foreach (var x in studentPreferences.SelectedSubjects)
                {
                    StudentSubject selectedSubject = new StudentSubject()
                    {
                        SubjectId = x.SubjectId,
                        StudentId = studentPreferences.studentId,
                        CreatedOn = DateTime.Now
                    };
                  var returnedSubjectId=  this._studentSubjectService.SaveStudentSubject(selectedSubject);
                  if (returnedSubjectId == 0)
                  {
                      subjectResult = new Result()
                      {
                          Message = "could not save the Subject with Id" + x.SubjectId,
                          Status = "Failed"
                      };
                  }
                }
            }


            var selectedYears = studentPreferences.SelectedYears;
            if (selectedYears != null)
            {

                foreach (var x in selectedYears)
                {
                    StudentYear selectedYear= new StudentYear()
                    {
                        YearId = x.YearId,
                        StudentId = studentPreferences.studentId,
                        CreatedOn = DateTime.Now
                    };
                   var returnedYearId= this._studentYearService.SaveStudentYear(selectedYear);
                   if (returnedYearId == 0)
                   {
                       yearResult = new Result()
                       {
                           Message = "could not save the Year with Id" + x.YearId,
                           Status = "Failed"
                       };
                   }
                }
            }

            if (studentPreferences.SelectedGroups!= null)
            {
                foreach (var x in studentPreferences.SelectedGroups)
                {
                    StudentGroup selectedGroup = new StudentGroup()
                    {
                        GroupId = x.GroupId,
                        StudentId = studentPreferences.studentId,
                        CreatedOn = DateTime.Now
                    };
                  var returnedGroupId= this._studentGroupService.SaveStudentGroup(selectedGroup);
                  if (returnedGroupId == 0)
                  {
                      groupResult = new Result()
                      {
                          Message = "could not save the  with Id" + x.GroupId,
                          Status = "Failed"
                      };
                  }
                }
            }

            if (yearResult.Status != "Failed" && courseResult.Status != "Failed" && groupResult.Status != "Failed" && subjectResult.Status != "Failed")
            {
                saved = false;
            }

            return saved;
        }

         
    }
}
