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
    public class ArticleTypeModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiArticleType/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public ArticleTypeModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(object ArticleTypeID = null, string ArticleTypeName = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (ArticleTypeID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ArticleTypeID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value =ArticleTypeID,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And

                });
            }
            if (ArticleTypeName != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ArticleTypeName",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value =ArticleTypeName,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
            var requestBody = new GenericDataFormat() { Filters = filters };
            if (fromView)
            {
                return new ArticleTypeModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new ArticleTypeModel<TModel>().Get(requestBody);
            }
        }
    }
    public class ArticleTypeViewModel : ArticleType
    {
    }

    public class ArticleTypeIndexViewModel : ArticleType
    {
    }

    public class ArticleTypeDetailsViewModel : ArticleTypeViewModel
    {
    }

    [Bind(Include = "ArticleTypeID,ArticleTypeName")]
    public class ArticleTypeCreateBindModel : ArticleType
    {
    }

    [Bind(Include = "ArticleTypeID,ArticleTypeName")]
    public class ArticleTypeEditBindModel : ArticleType
    {
    }
    public class ArticleTypeEditModel
    {
        public ArticleType EditItem { get; set; }
    }
}