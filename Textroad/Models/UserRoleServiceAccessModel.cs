using Textroad.Models;
using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using Security.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Security.Models
{
    public class UserRoleServiceAccessModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiUserRoleServiceAccessView/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public UserRoleServiceAccessModel() : base(ApiUrl, ApiRoute)
        {
        }

        public IEnumerable<TModel> GetData(Guid? userId = null, int? roleId = null, int? serviceId = null, int? accessTypeId = null, string controlTag = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();

            if (userId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "UserId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = userId.Value,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }

            if (roleId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "RoleId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = roleId,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And

                });
            }

            if (serviceId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ServiceId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = serviceId,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }

            if (accessTypeId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "AccessTypeId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = accessTypeId,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }

            if (controlTag != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "ControlTag",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = controlTag,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And
                });
            }

            //if (isBlock != null)
            //{
            //    filters.Add(new GenericDataFormat.FilterItems()
            //    {
            //        Property = "IsBlock",
            //        Operation = GenericDataFormat.FilterOperations.Equal,
            //        Value = isBlock,
            //        LogicalOperation = GenericDataFormat.LogicalOperations.And
            //    });
            //}

            var requestBody = new GenericDataFormat() { Filters = filters };
            if (fromView)
            {
                return new UserRoleServiceAccessModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new UserRoleServiceAccessModel<TModel>().Get(requestBody);
            }
        }
        internal IEnumerable<TModel> GetUserPermissiom(Guid userId, bool fromView = false)
        {
            return new UserRoleServiceAccessModel<TModel>().GetData(userId: userId, fromView: fromView);
        }
        public List<UserRoleServiceAccessViewModel> GetSavedUserPermission()
        {
            List<UserRoleServiceAccessViewModel> userPermissions = null;
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[UserRoleServiceAccessViewModel.SessionName] != null)
            {
                //string json_UserPermission = (string)HttpContext.Current.Session[UserRoleServiceAccessViewModel.SessionName];
                //userPermissions = (List<UserRoleServiceAccessViewModel>)Newtonsoft.Json.JsonConvert.DeserializeObject(json_UserPermission);
                userPermissions = (List<UserRoleServiceAccessViewModel>)HttpContext.Current.Session[UserRoleServiceAccessViewModel.SessionName];
            }
            else
            {
                var user = new UserViewModel().GetUserFromSession();
                userPermissions = new UserRoleServiceAccessModel<UserRoleServiceAccessViewModel>().GetUserPermissiom(user.UserId, true).ToList();
                SaveUserPermissionToLocalStorage(userPermissions, true);
            }

            return userPermissions;
        }
        public void SaveUserPermissionToLocalStorage(List<UserRoleServiceAccessViewModel> userPermissions, bool rememberMe)
        {
            string json_UserPermission = Newtonsoft.Json.JsonConvert.SerializeObject(userPermissions);
            System.Web.Security.FormsAuthentication.SetAuthCookie(json_UserPermission, rememberMe);
            //for test
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[UserRoleServiceAccessViewModel.SessionName] = userPermissions;
            }

        }
        public bool RemoveUserPermissionSession()
        {
            HttpContext.Current.Session.Remove(UserRoleServiceAccessViewModel.SessionName);
            return true;
        }
    }

    public class UserRoleServiceAccessViewModel : UserRoleServiceAccessView
    {
        public const string SessionName = "UserRoleServiceAccess";
    }

    public class UserRoleServiceAccessDetailsViewModel : UserRoleServiceAccessViewModel
    {

    }
}