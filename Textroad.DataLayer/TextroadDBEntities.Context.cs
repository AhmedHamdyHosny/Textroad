﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TextRoadDBEntities : DbContext
    {
        public TextRoadDBEntities()
            : base("name=TextRoadDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ConferenceManuscript> ConferenceManuscript { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Decision> Decision { get; set; }
        public virtual DbSet<EditoralBoard> EditoralBoard { get; set; }
        public virtual DbSet<EditorBoardPosition> EditorBoardPosition { get; set; }
        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<EmailAlert> EmailAlert { get; set; }
        public virtual DbSet<Guest> Guest { get; set; }
        public virtual DbSet<GuestEditor> GuestEditor { get; set; }
        public virtual DbSet<InvitationTemplate> InvitationTemplate { get; set; }
        public virtual DbSet<Journal> Journal { get; set; }
        public virtual DbSet<JournalReviewer> JournalReviewer { get; set; }
        public virtual DbSet<JournalScope> JournalScope { get; set; }
        public virtual DbSet<JournalSponsor> JournalSponsor { get; set; }
        public virtual DbSet<MainPage> MainPage { get; set; }
        public virtual DbSet<ManuscriptAuthor> ManuscriptAuthor { get; set; }
        public virtual DbSet<ManuscriptPhase> ManuscriptPhase { get; set; }
        public virtual DbSet<ManuscriptPhaseDecision> ManuscriptPhaseDecision { get; set; }
        public virtual DbSet<ManuscriptReview> ManuscriptReview { get; set; }
        public virtual DbSet<ManuscriptStatus> ManuscriptStatus { get; set; }
        public virtual DbSet<ManuscriptUser> ManuscriptUser { get; set; }
        public virtual DbSet<OpenSpecialIssue> OpenSpecialIssue { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PublishPeriod> PublishPeriod { get; set; }
        public virtual DbSet<RelationalJournal> RelationalJournal { get; set; }
        public virtual DbSet<Reviewer> Reviewer { get; set; }
        public virtual DbSet<Scope> Scope { get; set; }
        public virtual DbSet<SL_ActionLog> SL_ActionLog { get; set; }
        public virtual DbSet<SL_ErrorLog> SL_ErrorLog { get; set; }
        public virtual DbSet<Sponsor> Sponsor { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<ManuscriptView> ManuscriptView { get; set; }
        public virtual DbSet<JournalVolumeType> JournalVolumeType { get; set; }
        public virtual DbSet<JournalVersion> JournalVersion { get; set; }
        public virtual DbSet<JournalVersionView> JournalVersionView { get; set; }
        public virtual DbSet<JournalView> JournalView { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Conference> Conference { get; set; }
        public virtual DbSet<ConferenceView> ConferenceView { get; set; }
        public virtual DbSet<ManuscriptType> ManuscriptType { get; set; }
        public virtual DbSet<Manuscript> Manuscript { get; set; }
        public virtual DbSet<ManuscriptCharge> ManuscriptCharge { get; set; }
        public virtual DbSet<ArticleType> ArticleType { get; set; }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<PublishArticleAuthor> PublishArticleAuthor { get; set; }
        public virtual DbSet<PublishArticleScope> PublishArticleScope { get; set; }
        public virtual DbSet<PublishedArticle> PublishedArticle { get; set; }
        public virtual DbSet<UserView> UserView { get; set; }
        public virtual DbSet<PublishedArticleView> PublishedArticleView { get; set; }
    }
}