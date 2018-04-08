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
    public class PublishPeriodModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiPublishPeriod/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public PublishPeriodModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(Guid? PublishPeriodID = null, string PublishPeriodName = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
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
            if (PublishPeriodName != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "PublishPeriodName",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = PublishPeriodName,
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
                return new PublishPeriodModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new PublishPeriodModel<TModel>().Get(requestBody);
            }
        }
    }

    public class PublishPeriodViewModel : PublishPeriod
    {

        private UserViewModel _createUser = null;
        private UserViewModel _modifyUser = null;
        public UserViewModel CreateUser
        {
            get
            {
                if (_createUser == null)
                {
                    new UserModel<UserViewModel>().Get_Create_Modify_User(this.CreateUserId, this.ModifyUserId, ref this._createUser, ref this._modifyUser);
                }
                return _createUser;
            }
            set
            {
                _createUser = value;
            }
        }
        public UserViewModel ModifyUser
        {
            get
            {
                return _modifyUser;
            }
            set
            {
                _modifyUser = value;
            }
        }
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
        internal void BindCreate_Modify_User()
        {
            var tempUser = this.CreateUser;
        }


    }

    public class PublishPeriodIndexViewModel : PublishPeriod
    {
    }

    public class PublishPeriodDetailsViewModel : PublishPeriodViewModel
    {
    }

    [Bind(Include = "PublishPeriodID,Description")]
    public class PublishPeriodCreateBindModel : PublishPeriod
    {

    }

    [Bind(Include = "PublishPeriodID,Description,IsBlock,CreateUserId,CreateDate")]
    public class PublishPeriodEditBindModel : PublishPeriod
    {
    }
    public class PublishPeriodEditModel
    {
        public PublishPeriod EditItem { get; set; }
    }
}