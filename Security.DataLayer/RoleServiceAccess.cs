//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Security.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoleServiceAccess
    {
        public System.Guid RoleServiceAccessID { get; set; }
        public System.Guid RoleID { get; set; }
        public System.Guid ServiceAccessID { get; set; }
        public bool IsBlock { get; set; }
        public System.Guid CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.Guid> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual ServiceAccess ServiceAccess { get; set; }
    }
}