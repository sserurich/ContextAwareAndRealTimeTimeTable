using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Description { get; set; }
        public int ActivityId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public  Activity Activity { get; set; }
        public  AspNetUser AspNetUser { get; set; }
    }
}
