using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Textroad.DataLayer;

namespace Textroad.Models
{
    public class AuthorModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiAuthor/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public AuthorModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(object AuthorID = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (AuthorID != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "AuthorID",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = AuthorID,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }
            var requestBody = new GenericDataFormat() { Filters = filters };
            if (fromView)
            {
                return new AuthorModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new AuthorModel<TModel>().Get(requestBody);
            }
        }
    }

    public class AuthorViewModel : Author
    {
    }

    public class AuthorIndexViewModel : Author
    {
    }

    public class AuthorDetailsViewModel : AuthorViewModel
    {
        //private UserViewModel _createUser = null;
        //private UserViewModel _modifyUser = null;
        //public UserViewModel CreateUser
        //{
        //    get
        //    {
        //        if (_createUser == null)
        //        {
        //            new UserModel<UserViewModel>().Get_Create_Modify_User(this.CreateUserId, this.ModifyUserId, ref this._createUser, ref this._modifyUser);
        //        }
        //        return _createUser;
        //    }
        //    set
        //    {
        //        _createUser = value;
        //    }
        //}
        //public UserViewModel ModifyUser
        //{
        //    get
        //    {
        //        return _modifyUser;
        //    }
        //    set
        //    {
        //        _modifyUser = value;
        //    }
        //}
        //public string Block
        //{
        //    get
        //    {
        //        return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
        //    }
        //}
        //internal void BindCreate_Modify_User()
        //{
        //    var tempUser = this.CreateUser;
        //}
    }

    [Bind(Exclude = "IsBlock,CreateUserId,CreateDate,ModifyUserId,ModifyDate")]
    public class AuthorCreateBindModel : Author
    {

    }

    [Bind(Exclude = "ModifyUserId, ModifyDate")]
    public class AuthorEditBindModel : Author
    {
    }
    public class AuthorEditModel
    {
        public Author EditItem { get; set; }
    }
}