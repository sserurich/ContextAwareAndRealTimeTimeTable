using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.Models
{
    public class GroupActivity
    {
        public int GroupActivityId { get; set; }
        public int ActivityId { get; set; }
        public int GroupId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual Group Group { get; set; }
    }
}
