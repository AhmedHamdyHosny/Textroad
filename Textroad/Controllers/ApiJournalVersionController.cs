using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Textroad.DataLayer;

namespace Textroad.Controllers
{
    public class ApiJournalVersionController : BaseApiController<JournalVersion>
    {
        public override IHttpActionResult GetGridView(GenericDataFormat data)
        {
            var controller = new ApiJournalVersionViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetGridView(data);
        }
        public override IHttpActionResult GetView(GenericDataFormat data)
        {
            var controller = new ApiJournalVersionViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.GetView(data);
        }
        public override IHttpActionResult Export(GenericDataFormat data)
        {
            var controller = new ApiJournalVersionViewController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            return controller.Export(data);
        }
    }
    public class ApiJournalVersionViewController : BaseApiController<JournalVersionView>
    {
    }
}
