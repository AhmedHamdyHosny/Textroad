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

    public class JournalVolumeTypeModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiJournalVolumeType/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public JournalVolumeTypeModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(Guid? JournalVolumeTypeID = null, string JournalVolumeTypeName = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
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
            if (JournalVolumeTypeName != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "JournalVolumeTypeName",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = JournalVolumeTypeName,
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
                return new JournalVolumeTypeModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new JournalVolumeTypeModel<TModel>().Get(requestBody);
            }
        }
    }

    public class JournalVolumeTypeViewModel : JournalVolumeType
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
    }

    public class JournalVolumeTypeIndexViewModel : JournalVolumeType
    {
    }

    public class JournalVolumeTypeDetailsViewModel : SubjectViewModel
    {
    }

    [Bind(Include = "JournalVolumeTypeID,JournalVolumeTypeName")]
    public class JournalVolumeTypeCreateBindModel : JournalVolumeType
    {

    }

    [Bind(Include = "JournalVolumeTypeID,JournalVolumeTypeName,IsBlock,CreateUserId,CreateDate")]
    public class JournalVolumeTypeEditBindModel : JournalVolumeType
    {
    }
    public class JournalVolumeTypeEditModel
    {
        public JournalVolumeType EditItem { get; set; }
    }
}