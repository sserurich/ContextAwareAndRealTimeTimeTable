//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContextAwareAndRealTimeTimeTable.Demo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Activity
    {
        public Activity()
        {
            this.Comments = new HashSet<Comment>();
            this.GroupActivities = new HashSet<GroupActivity>();
        }
    
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public int LecturerId { get; set; }
        public int DayId { get; set; }
        public int RoomId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    
        public virtual Day Day { get; set; }
        public virtual Group Group { get; set; }
        public virtual Lecturer Lecturer { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<GroupActivity> GroupActivities { get; set; }
    }
}
