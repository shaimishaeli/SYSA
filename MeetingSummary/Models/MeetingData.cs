//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MeetingSummary.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MeetingData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingData()
        {
            this.MeetingAssignments = new HashSet<MeetingAssignments>();
            this.MeetingTasks = new HashSet<MeetingTasks>();
        }
    
        public int Id { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string MeetingSummary { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingAssignments> MeetingAssignments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingTasks> MeetingTasks { get; set; }
    }
}