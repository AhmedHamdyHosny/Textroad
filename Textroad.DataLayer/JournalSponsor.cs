//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Textroad.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class JournalSponsor
    {
        public System.Guid JournalSponserID { get; set; }
        public System.Guid JournalID { get; set; }
        public System.Guid SponsorID { get; set; }
        public string SponsorUrl { get; set; }
        public int SponsorPeriority { get; set; }
        public bool IsBlock { get; set; }
        public System.Guid CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.Guid> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual Journal Journal { get; set; }
        public virtual Sponsor Sponsor { get; set; }
    }
}
