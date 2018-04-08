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
    public class SubjectController : BaseController<Subject,SubjectViewModel,SubjectIndexViewModel,SubjectDetailsViewModel,SubjectCreateBindModel,SubjectEditBindModel,SubjectEditModel, Subject, SubjectModel<Subject>,SubjectModel<SubjectViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<SubjectDetailsViewModel> items)
        {
            filters = new List<GenericDataFormat.FilterItems>();
            filters.Add(new GenericDataFormat.FilterItems() { Property = "SubjectID", Operation = GenericDataFormat.FilterOperations.Equal, Value = id });
            var requestBody = new GenericDataFormat() { Filters = filters};
            items = new SubjectModel<SubjectDetailsViewModel>().Get(requestBody);
        }
        public override void FuncPreCreate(ref SubjectCreateBindModel model)
        {
            model.SubjectID = Guid.NewGuid();
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref Subject EditItem, ref SubjectEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new SubjectModel<Subject>().Get(id);
            }
            if (EditItem != null)
            {
                model = new SubjectEditModel();
                model.EditItem = EditItem;
            }
        }
        public override void FuncPreEdit(ref object id, ref SubjectEditBindModel EditItem)
        {
            id = EditItem.SubjectID;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        public override void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {
            ExportFileName = "Subjects.xlsx";
            string properties = "SubjectName,NoView,IsBlock";
            ExportRequestBody = new GenericDataFormat() { Includes = new GenericDataFormat.IncludeItems() { Properties = properties, } };
        }
    }
}