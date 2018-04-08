using Classes.Helper;
using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textroad.DataLayer;

namespace Textroad.Models
{
    public class JournalModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiJournal/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public JournalModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(object JournalID = null, object SubjectID = null, object PublishPeriodID = null, string ShortcutCharacter = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (JournalID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "JournalID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = JournalID,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
            if (SubjectID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "SubjectID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = SubjectID,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
            if (PublishPeriodID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "PublishPeriodID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = PublishPeriodID,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
            if (ShortcutCharacter != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ShortcutCharacter",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = ShortcutCharacter,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
            if (isBlock != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "IsBlock",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = isBlock,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And

                });
            }
            var requestBody = new GenericDataFormat() { Filters = filters };
            if (fromView)
            {
                return new JournalModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new JournalModel<TModel>().Get(requestBody);
            }
        }
    }

    public class JournalViewModel : JournalView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }

    public class JournalIndexViewModel : JournalView
    {
    }

    public class JournalDetailsViewModel : JournalViewModel
    {
    }

    [Bind(Include = "JournalID,SubjectID,PublishPeriodID,JournalName,ImagePath,BannerPath,NoView,ImpactFactor,About,AimsScope,ArticleProcessingCharge,PublicationEthics,ShortcutCharacter,EditorialBoard,AuthorGuidlines,OpenSpecialIssues,SpecialIssuesGuidlines,ReviewersAcknowledgment,ConderencePublication,PrintISSN,OnlineISSN,YearIssue")]
    public class JournalCreateBindModel : Journal
    {

    }

    [Bind(Include = "JournalID,SubjectID,PublishPeriodID,JournalName,ImagePath,BannerPath,NoView,ImpactFactor,About,AimsScope,ArticleProcessingCharge,PublicationEthics,ShortcutCharacter,EditorialBoard,AuthorGuidlines,OpenSpecialIssues,SpecialIssuesGuidlines,ReviewersAcknowledgment,ConderencePublication,PrintISSN,OnlineISSN,YearIssue,IsBlock,CreateUserId,CreateDate,ModifyUserId,ModifyDate")]
    public class JournalEditBindModel : Journal
    {
    }
    public class JournalEditModel
    {
        public Journal EditItem { get; set; }
    }
}