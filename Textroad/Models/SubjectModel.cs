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
    public class SubjectModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSubject/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public SubjectModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(Guid? SubjectID = null, string SubjectName = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
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
            if (SubjectName != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "SubjectName",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = SubjectName,
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
                return new SubjectModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new SubjectModel<TModel>().Get(requestBody);
            }
        }
    }

     public class SubjectViewModel : Subject
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

    public class SubjectIndexViewModel : Subject
    {
    }

    public class SubjectDetailsViewModel : SubjectViewModel
    {
    }

    [Bind(Include = "SubjectID,SubjectName")]
    public class SubjectCreateBindModel : Subject
    {

    }

    [Bind(Include = "SubjectID,SubjectName,NoView,IsBlock,CreateUserId,CreateDate")]
    public class SubjectEditBindModel : Subject
    {
    }
    public class SubjectEditModel
    {
        public Subject EditItem { get; set; }
    }
}