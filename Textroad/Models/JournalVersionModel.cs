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
    public class JournalVersionModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiJournalVersion/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public JournalVersionModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(object JournalVersionID = null, object JournalID = null, object JournalVolumeTypeID = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (JournalVersionID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "JournalVersionID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = JournalVersionID,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
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
            if (JournalVolumeTypeID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "JournalVolumeTypeID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = JournalVolumeTypeID,
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
                return new JournalVersionModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new JournalVersionModel<TModel>().Get(requestBody);
            }
        }
    }

    public class JournalVersionViewModel : JournalVersionView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }

    public class JournalVersionIndexViewModel : JournalVersionView
    {
    }

    public class JournalVersionDetailsViewModel : JournalVersionViewModel
    {
    }

    [Bind(Include = "JournalVersionID,JournalID,JournalVolumeTypeID,VersionNumber,IssueNumber,NOView,IssueDate,IssueName")]
    public class JournalVersionCreateBindModel : JournalVersion
    {

    }

    [Bind(Include = "JournalVersionID,JournalID,JournalVolumeTypeID,VersionNumber,IssueNumber,NOView,IssueDate,IssueName,IsBlock,CreateUserId,CreateDate")]
    public class JournalVersionEditBindModel : JournalVersion
    {
    }
    public class JournalVersionEditModel
    {
        public JournalVersion EditItem { get; set; }
    }
}