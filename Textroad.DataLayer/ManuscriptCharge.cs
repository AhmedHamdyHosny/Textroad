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
    
    public partial class ManuscriptCharge
    {
        public System.Guid ChargeID { get; set; }
        public System.Guid ManuscriptTypeID { get; set; }
        public System.Guid JournalID { get; set; }
        public decimal Cost { get; set; }
    
        public virtual Journal Journal { get; set; }
        public virtual ManuscriptType ManuscriptType { get; set; }
    }
}
