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
    public class NewsModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiNews/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public NewsModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(object NewsID = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (NewsID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "NewsID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = NewsID,
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
                return new NewsModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new NewsModel<TModel>().Get(requestBody);
            }
        }
    }

    public class NewsViewModel : News
    {
    }

    public class NewsIndexViewModel : News
    {

    }

    public class NewsDetailsViewModel : NewsViewModel
    {
        private string _CreateUser_FullName = null;
        private string _ModifyUser_FullName = null;
        public string CreateUser_FullName
        {
            get
            {
                if (_CreateUser_FullName == null)
                {
                    new UserModel<UserViewModel>().Get_Create_Modify_User(this.CreateUserId, this.ModifyUserId, ref this._CreateUser_FullName, ref this._CreateUser_FullName);
                }
                return _CreateUser_FullName;
            }
            set
            {
                _CreateUser_FullName = value;
            }
        }
        public string ModifyUser_FullName
        {
            get
            {
                if (_ModifyUser_FullName == null)
                {
                    new UserModel<UserViewModel>().Get_Create_Modify_User(this.CreateUserId, this.ModifyUserId, ref this._CreateUser_FullName, ref this._CreateUser_FullName);
                }
                return _ModifyUser_FullName;
            }
            set
            {
                _ModifyUser_FullName = value;
            }
        }
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }

    [Bind(Include = "NewsID,NewsTitle,NewsDetails,NewsImage")]
    public class NewsCreateBindModel : News
    {

    }

    [Bind(Include = "NewsID,NewsTitle,NewsDetails,NewsImage,NoViews,IsBlock,CreateUserId,CreateDate")]
    public class NewsEditBindModel : News
    {
    }
    public class NewsEditModel
    {
        public News EditItem { get; set; }
    }
}