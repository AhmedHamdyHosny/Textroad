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
    
    public partial class PublishedArticleView
    {
        public System.Guid PublishedArticleID { get; set; }
        public System.Guid JournalVersionID { get; set; }
        public Nullable<int> VersionNumber { get; set; }
        public Nullable<int> IssueNumber { get; set; }
        public string IssueName { get; set; }
        public string JournalName { get; set; }
        public System.Guid ArticleTypeID { get; set; }
        public string ArticleTypeName { get; set; }
        public string ArticleTitle { get; set; }
        public string Abstract { get; set; }
        public string ArticleContent { get; set; }
        public int NoView { get; set; }
        public string FilePath { get; set; }
        public System.DateTime RecievedDate { get; set; }
        public System.DateTime AcceptDate { get; set; }
        public System.DateTime PublishDate { get; set; }
        public string Url { get; set; }
        public Nullable<int> FromPage { get; set; }
        public Nullable<int> ToPage { get; set; }
        public string Note { get; set; }
        public bool IsBlock { get; set; }
        public System.Guid CreateUserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.Guid> ModifyUserId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string CreateUser_FullName { get; set; }
        public string ModifyUser_FullName { get; set; }
    }
}