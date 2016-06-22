using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.Models
{
    public class StudentPreferences
    {
        public int studentId { get; set; }
        public List<Group> SelectedGroups {get;set;}
        public List<Course> SelectedCourses { get; set; }
        public List<Year> SelectedYears { get; set; }
        public List<Subject> SelectedSubjects { get; set; }

    }
}
