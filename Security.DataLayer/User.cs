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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.UserRole = new HashSet<UserRole>();
            this.UserService = new HashSet<UserService>();
            this.UserServiceAccess = new HashSet<UserServiceAccess>();
        }
    
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Affilition { get; set; }
        public Nullable<System.Guid> CountryID { get; set; }
        public System.Guid UserTypeID { get; set; }
        public System.DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }
        public bool AllowAccess { get; set; }
    
        public virtual UserType UserType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserService> UserService { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserServiceAccess> UserServiceAccess { get; set; }
    }
}
