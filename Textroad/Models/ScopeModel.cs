using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Textroad.DataLayer;

namespace Textroad.Models
{
    public class ScopeModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiScope/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public ScopeModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(object ScopeID = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (ScopeID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ScopeID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = ScopeID,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And

                });
            }
            var requestBody = new GenericDataFormat() { Filters = filters };
            if (fromView)
            {
                return new ScopeModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new ScopeModel<TModel>().Get(requestBody);
            }
        }
    }

    public class ScopeViewModel : Scope
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

    public class ScopeIndexViewModel : Scope
    {
    }

    public class ScopeDetailsViewModel : ScopeViewModel
    {
    }

    [Bind(Include = "ScopeID,Description")]
    public class ScopeCreateBindModel : Scope
    {

    }

    [Bind(Include = "ScopeID,Description,IsBlock,CreateUserId,CreateDate")]
    public class ScopeEditBindModel : Scope
    {
    }
    public class ScopeEditModel
    {
        public Scope EditItem { get; set; }
    }
}