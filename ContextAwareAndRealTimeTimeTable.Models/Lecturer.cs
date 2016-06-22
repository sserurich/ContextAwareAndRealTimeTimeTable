using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        public string EmployeeNumber { get; set; }
        public string UserId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        //public  ICollection<Activity> Activities { get; set; }
        //public  AspNetUser AspNetUser { get; set; }
        //public  ICollection<Comment> Comments { get; set; }
    }
}
