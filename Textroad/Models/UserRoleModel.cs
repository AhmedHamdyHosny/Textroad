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
    public class UserRoleModel<TModel> : GenericModel<TModel> where TModel : class
    {
        const string ApiRoute = "api/ApiUserRole/";
        private static string ApiUrl = SiteConfig.ApiUrl;
        public UserRoleModel() : base(ApiUrl, ApiRoute)
        {
        }

        internal IEnumerable<TModel> GetData(int? userRoleId = null, int? userId = null, int? roleId = null, bool? isBlock = null, bool fromView = false)
        {
            var filters = new List<GenericDataFormat.FilterItems>();
            if (userRoleId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "UserRoleId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = userRoleId,
                    LogicalOperation = GenericDataFormat.LogicalOperations.And

                });
            }
            if (userId != null)
            {
                filters.Add(new GenericDataFormat.FilterItems()
                {
                    Property = "UserId",
                    Operation = GenericDataFormat.FilterOperations.Equal,
                    Value = userId,
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
                return new UserRoleModel<TModel>().GetView<TModel>(requestBody).PageItems;
            }
            else
            {
                return new UserRoleModel<TModel>().Get(requestBody);
            }
        }
    }
    public class UserRoleViewModel : UserRoleView
    {


    }
    public class UserRoleIndexViewModel : UserRoleView
    {

    }
    public class UserRoleDetailsViewModel : UserRoleViewModel
    {
        public string Block
        {
            get
            {
                return this.IsBlock ? Resources.Resource.True : Resources.Resource.False;
            }
        }
    }
}