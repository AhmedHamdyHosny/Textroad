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
    
    public partial class SL_ErrorLog
    {
        public System.Guid LogID { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public string Url { get; set; }
        public string ClientIP { get; set; }
        public string ErrorMessage { get; set; }
        public string UrlReferrer { get; set; }
    }
}