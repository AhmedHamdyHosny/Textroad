using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Textroad.Models;
using Security.Models;
using Classes.Common;
using Security.DataLayer;

namespace Classes.Utilities
{
    public class MyCustomVisibilityProvider : SiteMapNodeVisibilityProviderBase
    {
        
        public override bool IsVisible(ISiteMapNode node, IDictionary<string, object> sourceMetadata)
         {
            //get current user
            UserViewModel CurrentUser = new UserViewModel().GetUserFromSession();
            List<UserRoleServiceAccessViewModel> UserPermissions = new UserRoleServiceAccessModel<UserRoleServiceAccessViewModel>().GetSavedUserPermission();
            string nodeServiceTag = null;
            if (node.Attributes.Keys.Contains("serivceTag"))
            {
                nodeServiceTag = node.Attributes["serivceTag"].ToString();
            }
             
            bool isVisible = false;
            Guid userId = new Guid(sourceMetadata["UserId"].ToString());
            if (CurrentUser == null || (userId != null && CurrentUser.UserId != userId))
            {
                CurrentUser = new UserViewModel().GetUserFromSession();
            }

            if (CurrentUser != null) //&& user.EmployeeId != null
            {
                
                if (UserModel<User>.IsAdmin(CurrentUser))
                {
                    isVisible = true;
                }
                else
                {
                    if(node.Title == "Dashboard")
                    {
                        return true;
                    }
                    if (UserPermissions == null || (userId != null && CurrentUser.UserId != userId))
                    {
                        UserPermissions = new UserRoleServiceAccessModel<UserRoleServiceAccessViewModel>().GetData(userId: CurrentUser.UserId, fromView: true).ToList();
                    }

                    if (node.HasChildNodes)
                    {
                        //isVisible = true;
                        isVisible = node.ChildNodes.Where(x=>x.HasChildNodes == false).Any(x=> UserPermissions.Any(y => y.ServiceName == x.Title && y.AccessTypeId == DBEnums.AccessType.View));
                    }
                    else
                    {
                        isVisible = UserPermissions.Any(x => x.ServiceTag == nodeServiceTag && x.AccessTypeId == DBEnums.AccessType.View);
                    }
                }
            }
            return isVisible;
        }
    }
}