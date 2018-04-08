using Classes.Utilities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textroad.DataLayer;

namespace Textroad.Models
{
    public class SponsorModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiSponsor/";
        private static string ApiUrl = SiteConfig.ApiUrl;

        public SponsorModel() : base(ApiUrl, ApiRoute)
        {
        }
    }

    public class SponsorViewModel : Sponsor
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

    public class SponsorIndexViewModel : Sponsor
    {
    }

    public class SponsorDetailsViewModel : SponsorViewModel
    {
    }

    [Bind(Include = "SponsorID,SponsorName,Shortcut,Image")]
    public class SponsorCreateBindModel : Sponsor
    {

    }

    [Bind(Include = "SponsorID,SponsorName,Shortcut,Image,IsBlock,CreateUserId,CreateDate")]
    public class SponsorEditBindModel : Sponsor
    {
    }
    public class SponsorEditModel
    {
        public Sponsor EditItem { get; set; }
    }
}