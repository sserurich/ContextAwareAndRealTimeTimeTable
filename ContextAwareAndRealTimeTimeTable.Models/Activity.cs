using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public int LecturerId { get; set; }
        public int DayId { get; set; }
        public int RoomId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Type { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public  Day Day { get; set; }
        public  Group Group { get; set; }
        public Lecturer Lecturer { get; set; }
        public  Room Room { get; set; }
        public  Subject Subject { get; set; }
        public  ICollection<Comment> Comments { get; set; }
        public  ICollection<GroupActivity> GroupActivities { get; set; }
    }
}
