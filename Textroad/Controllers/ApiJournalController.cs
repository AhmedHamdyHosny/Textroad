using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericApiController.Utilities;
using Textroad.DataLayer;

namespace Textroad.Controllers
{
    public class ApiJournalController : BaseApiController<Journal>
    {
        public override IHttpActionResult Export(GenericDataFormat data)
        {
            var controller = new ApiJournalViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
        public override IHttpActionResult GetGridView(GenericDataFormat data)
        {
            var controller = new ApiJournalViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetGridView(data);
        }
        public override IHttpActionResult GetView(GenericDataFormat data)
        {
            var controller = new ApiJournalViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }
    }

    public class ApiJournalViewController : BaseApiController<JournalView>
    {
    }
}
