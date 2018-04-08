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
    public class PublishedArticleModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiPublishedArticle/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public PublishedArticleModel() : base(ApiUrl, ApiRoute)
        {
        }
        internal IEnumerable<TModel> GetData(object ArticleID = null, object JournalVersionID = null, object ArticleTypeID = null, string ArticleTitle = null, string Abstract = null, string ArticleContent = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (ArticleID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ArticleID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = ArticleID,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
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
            if (ArticleTypeID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ArticleTypeID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = ArticleTypeID,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
            if (ArticleTitle != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ArticleTitle",
                    Operation = GenericDataFormat.FilterOperations.Like,
                    Value = ArticleTitle,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
            if (Abstract != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "Abstract",
                    Operation = GenericDataFormat.FilterOperations.Like,
                    Value = Abstract,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
            if (ArticleContent != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ArticleContent",
                    Operation = GenericDataFormat.FilterOperations.Like,
                    Value = ArticleContent,
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
                return new PublishedArticleModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new PublishedArticleModel<TModel>().Get(requestBody);
            }
        }
    }

    public class PublishedArticleViewModel : PublishedArticleView
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }

    public class PublishedArticleIndexViewModel : PublishedArticleView
    {
    }

    public class PublishedArticleDetailsViewModel : PublishedArticleViewModel
    {
    }

    [Bind(Exclude = "IsBlock,CreateUserId,CreateDate,ModifyUserId,ModifyDate")]
    public class PublishedArticleCreateBindModel : PublishedArticle
    {

    }

    [Bind(Exclude = "ModifyUserId,ModifyDate")]
    public class PublishedArticleEditBindModel : PublishedArticle
    {
    }
    public class PublishedArticleEditModel
    {
        public PublishedArticle EditItem { get; set; }
    }
}