using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.Models
{
    public class CourseSubject
    {
        public int CourseSubjectId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public  Course Course { get; set; }
        public Subject Subject { get; set; }
    }
}
