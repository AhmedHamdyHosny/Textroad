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
    public class ConferenceModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiConference/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public ConferenceModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(object ConferenceID = null, object SubjectID = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (ConferenceID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ConferenceID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = ConferenceID,
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
                return new ConferenceModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new ConferenceModel<TModel>().Get(requestBody);
            }
        }
    }

    public class ConferenceViewModel : ConferenceView
    {
        
    }

    public class ConferenceIndexViewModel : ConferenceViewModel
    {
    }

    public class ConferenceDetailsViewModel : ConferenceViewModel
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }

    [Bind(Include = "ConferenceID,SubjectID,ConferenceName,ConferenceDetails,StartDate,Duration,Country,City,Address,Photo,Url")]
    public class ConferenceCreateBindModel : Conference
    {

    }

    [Bind(Include = "ConferenceID,SubjectID,ConferenceName,ConferenceDetails,StartDate,Duration,Country,City,Address,Photo,Url,IsBlock,CreateUserId,CreateDate")]
    public class ConferenceEditBindModel : Conference
    {
    }
    public class ConferenceEditModel
    {
        public Conference EditItem { get; set; }
    }
}