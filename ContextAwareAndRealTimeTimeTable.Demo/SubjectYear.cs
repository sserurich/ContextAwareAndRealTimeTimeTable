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
    
    public partial class SubjectYear
    {
        public int SubjectYearId { get; set; }
        public int SubjectId { get; set; }
        public int YearId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<System.DateTime> DeletedOn { get; set; }
    
        public virtual Subject Subject { get; set; }
        public virtual Year Year { get; set; }
    }
}
