using Security.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Textroad.DataLayer;

namespace Security.Controllers
{
    public class ApiUserController : BaseSecurityApiController<User>
    {
        public override IHttpActionResult GetGridView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiUserViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }
        public override IHttpActionResult GetView(GenericApiController.Utilities.GenericDataFormat data)
        {
            var controller = new ApiUserViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }

        public override IHttpActionResult Put(Guid id, [FromBody] User value)
        {
            GetAuthorization();
            if (!IsAuthorize(GenericApiController.Utilities.Actions.Put))
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }

            var item = repo.Repo.GetByID(id, filter: GetDataConstrains());
            if (item != null)
            {
                using (var context = new SecurityEntities())
                {
                    var originalItem = context.User
                        //.Include(j => j.UserService)
                        //.Include(j => j.UserServiceAccess)
                        //.Include(j => j.UserRole)
                        .Single(j => j.UserId == value.UserId);

                    // Update scalar/complex properties
                    context.Entry(originalItem).CurrentValues.SetValues(value);

                    // Update references
                    // Update UserService
                    if (value.UserService != null)
                    {
                        foreach (var childItem in value.UserService)
                        {
                            var originalDetailsItem = originalItem.UserService
                                .Where(c => c.UserServiceID == childItem.UserServiceID && c.UserServiceID != null)
                                .SingleOrDefault();
                            // Is original child item with same ID in DB?
                            if (originalDetailsItem != null)
                            {
                                childItem.CreateUserId = originalDetailsItem.CreateUserId;
                                childItem.CreateDate = originalDetailsItem.CreateDate;
                                context.Entry(originalDetailsItem).CurrentValues.SetValues(childItem);
                            }
                            else
                            {
                                childItem.UserServiceID = Guid.NewGuid();
                                originalItem.UserService.Add(childItem);
                            }
                        }

                        // Don't consider the child items we have just added above.
                        // (We need to make a copy of the list by using .ToList() because
                        // _dbContext.ChildItems.Remove in this loop does not only delete
                        // from the context but also from the child collection. Without making
                        // the copy we would modify the collection we are just interating
                        // through - which is forbidden and would lead to an exception.)
                        foreach (var originalChildItem in
                                     originalItem.UserService.Where(c => c.UserServiceID != null).ToList())
                        {
                            // Are there child items in the DB which are NOT in the
                            // new child item collection anymore?
                            if (!value.UserService.Any(c => c.UserServiceID == originalChildItem.UserServiceID))
                                // Yes -> It's a deleted child item -> Delete
                                context.UserService.Remove(originalChildItem);
                        }
                    }


                    //update UserServiceAccess
                    if (value.UserServiceAccess != null)
                    {
                        foreach (var childItem in value.UserServiceAccess)
                        {
                            var originalDetailsItem = originalItem.UserServiceAccess
                                .Where(c => c.UserServiceAccessID == childItem.UserServiceAccessID && c.UserServiceAccessID != null)
                                .SingleOrDefault();
                            // Is original child item with same ID in DB?
                            if (originalDetailsItem != null)
                            {
                                childItem.CreateUserId = originalDetailsItem.CreateUserId;
                                childItem.CreateDate = originalDetailsItem.CreateDate;
                                context.Entry(originalDetailsItem).CurrentValues.SetValues(childItem);
                            }
                            else
                            {
                                childItem.UserServiceAccessID = Guid.NewGuid();
                                originalItem.UserServiceAccess.Add(childItem);
                            }
                        }

                        foreach (var originalChildItem in
                                     originalItem.UserServiceAccess.Where(c => c.UserServiceAccessID != null).ToList())
                        {
                            // Are there child items in the DB which are NOT in the
                            // new child item collection anymore?
                            if (!value.UserServiceAccess.Any(c => c.UserServiceAccessID == originalChildItem.UserServiceAccessID))
                                // Yes -> It's a deleted child item -> Delete
                                context.UserServiceAccess.Remove(originalChildItem);
                        }
                    }

                    //update UserRole
                    if (value.UserRole != null)
                    {
                        foreach (var childItem in value.UserRole)
                        {
                            var originalDetailsItem = originalItem.UserRole
                                .Where(c => c.UserRoleId == childItem.UserRoleId && c.UserRoleId != null)
                                .SingleOrDefault();
                            // Is original child item with same ID in DB?
                            if (originalDetailsItem != null)
                            {
                                childItem.CreateUserId = originalDetailsItem.CreateUserId;
                                childItem.CreateDate = originalDetailsItem.CreateDate;
                                context.Entry(originalDetailsItem).CurrentValues.SetValues(childItem);
                            }
                            else
                            {
                                childItem.UserRoleId = Guid.NewGuid();
                                originalItem.UserRole.Add(childItem);
                            }
                        }

                        foreach (var originalChildItem in
                                     originalItem.UserRole.Where(c => c.UserRoleId != null).ToList())
                        {
                            // Are there child items in the DB which are NOT in the
                            // new child item collection anymore?
                            if (!value.UserRole.Any(c => c.UserRoleId == originalChildItem.UserRoleId))
                                // Yes -> It's a deleted child item -> Delete
                                context.UserRole.Remove(originalChildItem);
                        }
                    }

                    context.SaveChanges();
                }
                return Content(HttpStatusCode.OK, value);
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, "Unauthorized");
            }

        }
    }

    public class ApiUserViewController : Textroad.Controllers.BaseApiController<UserView>
    {
    }
}