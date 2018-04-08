using GenericApiController.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textroad.DataLayer;
using Textroad.Models;

namespace Textroad.Controllers
{
    public class PublishPeriodController : BaseController<PublishPeriod, PublishPeriodViewModel, PublishPeriodIndexViewModel, PublishPeriodDetailsViewModel, PublishPeriodCreateBindModel, PublishPeriodEditBindModel, PublishPeriodEditModel, PublishPeriod, PublishPeriodModel<PublishPeriod>, PublishPeriodModel<PublishPeriodViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<PublishPeriodDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "PublishPeriodID", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new PublishPeriodModel<PublishPeriodDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreCreate(ref PublishPeriodCreateBindModel model)
        {
            model.PublishPeriodID = Guid.NewGuid();
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref PublishPeriod EditItem, ref PublishPeriodEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new PublishPeriodModel<PublishPeriod>().Get(id);
            }
            if (EditItem != null)
            {
                model = new PublishPeriodEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref PublishPeriodEditBindModel EditItem)
        {
            id = EditItem.PublishPeriodID;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "Publish Periods.xlsx";
            string properties = "PublishPeriod,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}