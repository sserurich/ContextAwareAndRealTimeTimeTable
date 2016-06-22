using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.Models
{
    public class StudentYear
    {
        public int StudentYearId { get; set; }
        public int StudentId { get; set; }
        public int YearId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }

        public  Student Student { get; set; }
        public  Year Year { get; set; }
    }
}
