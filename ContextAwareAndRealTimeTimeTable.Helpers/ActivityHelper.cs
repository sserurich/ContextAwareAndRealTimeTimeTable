using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;
using ContextAwareAndRealTimeTimeTable.EF;

namespace ContextAwareAndRealTimeTimeTable.Helpers
{
   public class ActivityHelper
    {
        public static Subject CreateActivitySubject(EF.Models.Subject subject)
        {
            var foundSubject = new Models.Subject();
            if (subject != null)
            {
                foundSubject.Name = subject.Name;
                foundSubject.SubjectId = subject.SubjectId;              
            }
            return foundSubject;
        }

        public static Room CreateActivityRoom(EF.Models.Room room)
        {
            var foundRoom = new Models.Room();
            if (room != null)
            {
                foundRoom.Name = room.Name;
                foundRoom.RoomId = room.RoomId;
            }
            return foundRoom;
        }

        public static Lecturer CreateActivityLecturer(EF.Models.Lecturer lecturer)
        {
            var foundLecturer = new Models.Lecturer();
            if (lecturer != null)
            {
                foundLecturer.EmployeeNumber = lecturer.EmployeeNumber;
                foundLecturer.LecturerId = lecturer.LecturerId;
                foundLecturer.UserId = lecturer.UserId;
            }
            return foundLecturer;
        }

        public static Day CreateActivityDay(EF.Models.Day day)
        {
            var foundDay = new Models.Day();
            if (day != null)
            {
                foundDay.Name = day.Name;
                foundDay.DayId = day.DayId;
            }
            return foundDay;
        }

        public static Group CreateActivityGroup(EF.Models.Group group)
        {
            var foundGroup = new Models.Group();
            if (group != null)
            {
                foundGroup.Name = group.Name;
                foundGroup.GroupId = group.GroupId;
                foundGroup.YearId = group.YearId;
                foundGroup.CourseId = group.CourseId;
            }
            return foundGroup;
        }

        public static List<Comment> CreateActivityComments(ICollection<EF.Models.Comment> activityComments)
        {
            List<Comment> comments = new List<Comment>();
            if (activityComments != null)
            {
                foreach(var comment in activityComments){
                    comments.Add(new Comment()
                    {
                        CommentId = comment.CommentId,
                        Description= comment.Description,
                        CreatedOn  = comment.CreatedOn,
                        ActivityId = comment.ActivityId,
                        CreatedBy = comment.CreatedBy,
                        AspNetUser = new AspNetUser() { FirstName = comment.AspNetUser.FirstName, LastName= comment.AspNetUser.LastName,UserName = comment.AspNetUser.UserName, Id = comment.AspNetUser.Id}
                    });
                }
                
            }
            return comments;
            
        }
    }
}
