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
    
    public partial class JournalReviewer
    {
        public System.Guid JournalReviewerID { get; set; }
        public System.Guid JournalID { get; set; }
        public System.Guid ReviewerID { get; set; }
    
        public virtual Journal Journal { get; set; }
        public virtual Reviewer Reviewer { get; set; }
    }
}
