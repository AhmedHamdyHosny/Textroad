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
    
    public partial class JournalVersionView
    {
        public System.Guid JournalVersionID { get; set; }
        public System.Guid JournalID { get; set; }
        public string JournalName { get; set; }
        public System.Guid JournalVolumeTypeID { get; set; }
        public string JournalVolumeTypeName { get; set; }
        public int VersionNumber { get; set; }
        public int IssueNumber { get; set; }
        public int NoView { get; set; }
        public string IssueName { get; set; }
        public System.DateTime IssueDate { get; set; }
        public string IssueDate_Str { get; set; }
        public bool IsBlock { get; set; }
        public System.Guid CreateUserId { get; set; }
        public string CreateUser_FullName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.Guid> ModifyUserId { get; set; }
        public string ModifyUser_FullName { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
