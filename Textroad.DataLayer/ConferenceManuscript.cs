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
    
    public partial class ConferenceManuscript
    {
        public System.Guid ConferenceArticleID { get; set; }
        public Nullable<System.Guid> ConferenceID { get; set; }
        public System.Guid ManuscriptID { get; set; }
    
        public virtual Conference Conference { get; set; }
        public virtual Manuscript Manuscript { get; set; }
    }
}