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
    
    public partial class AccessType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccessType()
        {
            this.ServiceAccess = new HashSet<ServiceAccess>();
        }
    
        public System.Guid AccessTypeID { get; set; }
        public string AccessTypeName { get; set; }
        public bool IsBlock { get; set; }
        public System.Guid CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.Guid> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceAccess> ServiceAccess { get; set; }
    }
}
