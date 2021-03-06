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
    
    public partial class JournalVersion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JournalVersion()
        {
            this.GuestEditor = new HashSet<GuestEditor>();
        }
    
        public System.Guid JournalVersionID { get; set; }
        public System.Guid JournalID { get; set; }
        public System.Guid JournalVolumeTypeID { get; set; }
        public int VersionNumber { get; set; }
        public int IssueNumber { get; set; }
        public int NoView { get; set; }
        public System.DateTime IssueDate { get; set; }
        public string IssueName { get; set; }
        public bool IsBlock { get; set; }
        public System.Guid CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.Guid> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuestEditor> GuestEditor { get; set; }
        public virtual Journal Journal { get; set; }
        public virtual JournalVolumeType JournalVolumeType { get; set; }
    }
}
