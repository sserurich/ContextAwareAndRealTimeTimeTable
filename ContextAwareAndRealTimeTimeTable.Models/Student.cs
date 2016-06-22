using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string UserId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public  AspNetUser AspNetUser { get; set; }
        public  ICollection<StudentGroup> StudentGroups { get; set; }
        public  ICollection<StudentSubject> StudentSubjects { get; set; }
        public  ICollection<StudentYear> StudentYears { get; set; }
    }
}
