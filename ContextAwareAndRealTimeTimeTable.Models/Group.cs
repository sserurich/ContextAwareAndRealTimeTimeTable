using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
        public int YearId { get; set; }
        public Nullable<int> CourseId { get; set; }

        public ICollection<GroupActivity> GroupActivities { get; set; }
        public  ICollection<StudentGroup> StudentGroups { get; set; }
        public  Course Course { get; set; }
        public  Year Year { get; set; }
       // public virtual ICollection<CourseGroupSubject> CourseGroupSubjects { get; set; }
        public  ICollection<Activity> Activities { get; set; }
    }
}
