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
    public class ManuscriptTypeModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiManuscriptType/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public ManuscriptTypeModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(object ManuscriptTypeID = null, string ManuscriptTypeName = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (ManuscriptTypeID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ManuscriptTypeID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = ManuscriptTypeID,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And

                });
            }
            if (ManuscriptTypeName != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ManuscriptTypeName",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = ManuscriptTypeName,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
            var requestBody = new GenericDataFormat() { Filters = filters };
            if (fromView)
            {
                return new ManuscriptTypeModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new ManuscriptTypeModel<TModel>().Get(requestBody);
            }
        }
    }
    public class ManuscriptTypeViewModel : ManuscriptType
    {
    }

    public class ManuscriptTypeIndexViewModel : ManuscriptType
    {
    }

    public class ManuscriptTypeDetailsViewModel : ManuscriptTypeViewModel
    {
    }

    [Bind(Include = "ManuscriptTypeID,ManuscriptTypeName")]
    public class ManuscriptTypeCreateBindModel : ManuscriptType
    {
    }

    [Bind(Include = "ManuscriptTypeID,ManuscriptTypeName")]
    public class ManuscriptTypeEditBindModel : ManuscriptType
    {
    }
    public class ManuscriptTypeEditModel
    {
        public ManuscriptType EditItem { get; set; }
    }
}