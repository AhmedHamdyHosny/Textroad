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
    public class SponsorController : BaseController<Sponsor,SponsorViewModel, SponsorIndexViewModel, SponsorDetailsViewModel, SponsorCreateBindModel, SponsorEditBindModel, SponsorEditModel, Sponsor, SponsorModel<Sponsor>, SponsorModel<SponsorViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<SponsorDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SponsorID", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new SponsorModel<SponsorDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreCreate(ref SponsorCreateBindModel model)
        {
            model.SponsorID = Guid.NewGuid();
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref Sponsor EditItem, ref SponsorEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new SponsorModel<Sponsor>().Get(id);
            }
            if (EditItem != null)
            {
                model = new SponsorEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref SponsorEditBindModel EditItem)
        {
            id = EditItem.SponsorID;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "Sponsors.xlsx";
            string properties = "SponsorName,Shortcut,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}