using GenericApiController.Utilities;
using Security.DataLayer;
using System.Net.Http;
using System.Web.Http;

namespace Security.Controllers
{
    public class ApiUserRoleController : BaseSecurityApiController<UserRole>
    {
        public override IHttpActionResult GetGridView(GenericDataFormat data)
        {
            var controller = new ApiUserRoleViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetGridView(data);
        }
        public override IHttpActionResult GetView(GenericDataFormat data)
        {
            var controller = new ApiUserRoleViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }
    }
    public class ApiUserRoleViewController : BaseSecurityApiController<UserRoleView>
    {

    }
}
