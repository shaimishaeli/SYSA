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
    
    public partial class MeetingTasks
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    
        public virtual MeetingData MeetingData { get; set; }
    }
}
