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
    
    public partial class Email
    {
        public System.Guid EmailID { get; set; }
        public string PersonEmail { get; set; }
        public string PersonName { get; set; }
        public Nullable<System.Guid> CountryID { get; set; }
        public Nullable<System.Guid> ScopeID { get; set; }
        public string Affiliation { get; set; }
        public Nullable<bool> Vip { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual Scope Scope { get; set; }
    }
}
