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
    public class ScopeController : BaseController<Scope, ScopeViewModel, ScopeIndexViewModel, ScopeDetailsViewModel, ScopeCreateBindModel, ScopeEditBindModel, ScopeEditModel, Scope, ScopeModel<Scope>, ScopeModel<ScopeViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<ScopeDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "ScopeID", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters };
            items = new ScopeModel<ScopeDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreCreate(ref ScopeCreateBindModel model)
        {
            model.ScopeID = Guid.NewGuid();
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref Scope EditItem, ref ScopeEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new ScopeModel<Scope>().Get(id);
            }
            if (EditItem != null)
            {
                model = new ScopeEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref ScopeEditBindModel EditItem)
        {
            id = EditItem.ScopeID;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "Scopes.xlsx";
            string properties = "Description,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}