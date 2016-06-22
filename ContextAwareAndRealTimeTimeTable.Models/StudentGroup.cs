using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.Models
{
    public class StudentGroup
    {
        public int StudentGroupId { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public Group Group { get; set; }
        public Student Student { get; set; }
    }
}
