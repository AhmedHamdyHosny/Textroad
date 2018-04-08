using Classes.Utilities;
using Models;
using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Classes.Common.Enums;
using Classes.Common;
using Security.DataLayer;
using System.Web.Mvc;
using Security.Models;
using Textroad.DataLayer;

namespace Textroad.Models
{
    public class UserModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiUser/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public UserModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal void Get_Create_Modify_User(Guid createUserId, Guid? modifyUserId, ref UserViewModel createUser, ref UserViewModel modifyUser)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (createUserId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "UserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = createUserId, LogicalOperation = GenericDataFormat.LogicalOperations.Or });
            }

            if (modifyUserId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "UserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = createUserId });
            }
            var requestBody = new GenericDataFormat() { Filters = filters };
            var users = this.GetView<UserViewModel>(requestBody).PageItems;

            createUser = users.SingleOrDefault(x => x.UserId == createUserId);
            if (modifyUserId != null)
            {
                modifyUser = users.SingleOrDefault(x => x.UserId == modifyUserId);
            }
        }

        internal void Get_Create_Modify_User(Guid createUserId, Guid? modifyUserId, ref string CreateUser_FullName, ref string ModifyUser_FullName)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (createUserId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "UserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = createUserId, LogicalOperation = GenericDataFormat.LogicalOperations.Or });
            }

            if (modifyUserId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems() { Property = "UserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = createUserId });
            }
            var requestBody = new GenericDataFormat() { Filters = filters };
            var users = this.GetView<UserViewModel>(requestBody).PageItems;

            var createUser = users.SingleOrDefault(x => x.UserId == createUserId);
            if(createUser != null)
            {
                CreateUser_FullName = createUser.FullName;
            }
            if (modifyUserId != null)
            {
                var modifyUser = users.SingleOrDefault(x => x.UserId == modifyUserId);
                if(modifyUser != null)
                {
                    ModifyUser_FullName = modifyUser.FullName;
                }
            }
        }

        internal List<TModel> GetByView(int id)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "UserId",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = id
            });
            var requestBody = new GenericDataFormat() { Filters = filters };
            return this.GetView<TModel>(requestBody).PageItems;
        }

        public static string GetHashPassword(string userName, string password)
        {
            return SecurityMethods.Hashing(userName, password);
        }

        internal void ChangePassword(object userId, string password, int modifyUserId)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems()
            {
                Property = "UserId",
                Operation = GenericDataFormat.FilterOperations.Equal,
                Value = userId,
                LogicalOperation = GenericDataFormat.LogicalOperations.And
            });
            var includes = new GenericDataFormat.IncludeItems() { References = "UserService,UserServiceAccess,UserRole" };
            var editUser = new UserModel<User>().Get(new GenericDataFormat() { Filters = filters, Includes = includes }).SingleOrDefault();
            editUser.Password = UserModel<User>.GetHashPassword(editUser.UserName, password);
            new UserModel<User>().Update(editUser, editUser.UserId);
        }

        internal static bool IsAdmin(UserViewModel user)
        {
            return (user.UserTypeID == DBEnums.UserType.Admin );
        }


        //internal void Get_Create_User(int createUserId,ref UserViewModel createUser)
        //{
        //    var filters = new List<GenericDataFormat.FilterItems>();
        //    if (createUserId != 0)
        //    {
        //        filters.Add(new GenericDataFormat.FilterItems() { Property = "UserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = createUserId, LogicalOperation = GenericDataFormat.LogicalOperations.Or });
        //    }
        //    var requestBody = new GenericDataFormat() { Filters = filters };
        //    var users = this.Get(requestBody);
        //    createUser = users.Cast<UserViewModel>().SingleOrDefault(x => x.UserId == createUserId);
        //}
    }

    public class UserViewModel : UserView
    {
        public const string SessionName = "CurrentUser";
        public UserViewModel Login()
        {
            GenericDataFormat requestBody = new GenericDataFormat();
            requestBody.Filters = new List<GenericDataFormat.FilterItems>();
            requestBody.Filters.Add(new GenericDataFormat.FilterItems() { Property = "UserName", Value = this.UserName, Operation = GenericDataFormat.FilterOperations.Equal });
            UserViewModel user = new UserModel<UserViewModel>().GetView<UserViewModel>(requestBody).PageItems.SingleOrDefault();
            if (user != null)
            {
                //var pass = UserModel<UserViewModel>.GetHashPassword(this.UserName, this.Password);
                if (user.Password == UserModel<UserViewModel>.GetHashPassword(this.UserName, this.Password))
                {
                    return user;
                }
            }

            return null;
        }

        public void SaveUserToLocalStorage(bool rememberMe)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie(this.UserName, rememberMe);
            System.Web.Security.FormsAuthentication.SetAuthCookie(this.UserId.ToString(), rememberMe);
            //for test
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[UserViewModel.SessionName] = this;
            }

        }

        public UserViewModel GetUserFromSession()
        {
            UserViewModel cUser = null;
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[UserViewModel.SessionName] != null)
            {
                cUser = (UserViewModel)HttpContext.Current.Session[UserViewModel.SessionName];
            }

            //for test 
            if (cUser == null)
            {
                var requestBody = new GenericDataFormat();
                requestBody.Filters = new List<GenericDataFormat.FilterItems>();
                requestBody.Filters.Add(new GenericDataFormat.FilterItems() { Property = "UserId", Operation = GenericDataFormat.FilterOperations.Equal, Value = "5C8FBE12-69B2-4EB7-B116-201AEEBD862B" });
                cUser = new UserModel<UserViewModel>().GetView<UserViewModel>(requestBody).PageItems.SingleOrDefault();
                cUser.SaveUserToLocalStorage(true);
            }

            return cUser;

        }

        public bool RemoveUserSession()
        {
            HttpContext.Current.Session.Remove(UserViewModel.SessionName);
            var tUser = (UserViewModel)HttpContext.Current.Session[UserViewModel.SessionName];
            return true;
        }
    }

    public class UserIndexViewModel : UserView
    {

    }
    public class UserDetailsViewModel : UserViewModel
    {
        public string AllowAccess_Str
        {
            get
            {
                return this.AllowAccess == true ? Resources.Resource.True : Resources.Resource.False;
            }
        }
        public List<UserRoleViewModel> UserRoles { get; set; }
    }
    [Bind(Include = "UserId,UserName,Password,FirstName_en,FirstName_ar,LastName_en,LastName_ar,CountryId,UserTypeId,AllowAccess,UserRole")]
    public class UserCreateBindModel : User
    {
        public int? Employee_aux_id { get; set; }
    }
    [Bind(Include = "UserId,UserName,Password,FirstName_en,FirstName_ar,LastName_en,LastName_ar,CountryId,UserTypeId,AllowAccess,IsBlock,CreateUserId,CreateDate,UserRole")]
    public class UserEditBindModel : User
    {

    }
}