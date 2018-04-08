using Classes.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textroad.DataLayer;
using Textroad.Models;
using GenericApiController.Utilities;

namespace Textroad.Controllers
{
    public class ConferenceController : BaseController<Conference, ConferenceViewModel, ConferenceIndexViewModel, ConferenceDetailsViewModel, ConferenceCreateBindModel, ConferenceEditBindModel, ConferenceEditModel, Conference, ConferenceModel<Conference>, ConferenceModel<ConferenceViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<ConferenceDetailsViewModel> items)
        {
            items = new ConferenceModel<ConferenceDetailsViewModel>().GetData(ConferenceID: id, fromView: true).ToList();
        }
        public override void FuncPreInitCreateView()
        {
            base.FuncPreInitCreateView();
            var subjects = new SubjectModel<Subject>().GetData(isBlock: false);
            ViewBag.SubjectID = subjects.Select(x => new CustomSelectListItem() { Text = x.SubjectName, Value = x.SubjectID.ToString() }).ToList();
        }
        public override void FuncPreCreate(ref ConferenceCreateBindModel model)
        {
            model.ConferenceID = Guid.NewGuid();
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        [ActionName("Create")]
        public ActionResult CreateConference()
        {
            return base.Create();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateConference(ConferenceCreateBindModel model, HttpPostedFileBase file)
        {
            if(file != null)
            {
                string path = Server.MapPath(Classes.Utilities.SiteConfig.UploadFilesPath);
                string fileName = model.ConferenceName;
                Classes.Utilities.Utility.SaveFile(file, path, "Conference", ref fileName);
                model.ImageName = fileName;
            }
            return base.Create(model);
        }
        public override void FuncPreInitEditView(object id, ref Conference EditItem, ref ConferenceEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new ConferenceModel<Conference>().Get(id);
            }
            if (EditItem != null)
            {
                model = new ConferenceEditModel();
                model.EditItem = EditItem;
                var subjects = new SubjectModel<Subject>().GetData(isBlock: false);
                ViewBag.SubjectID = subjects.Select(x => new CustomSelectListItem() { Text = x.SubjectName, Value = x.SubjectID.ToString() }).ToList();
            }
        }
        public override void FuncPreEdit(ref object id, ref ConferenceEditBindModel EditItem)
        {
            id = EditItem.ConferenceID;
            var item = new ConferenceModel<Conference>().GetData(ConferenceID: id).SingleOrDefault();
            EditItem.CreateUserId = item.CreateUserId;
            EditItem.CreateDate = item.CreateDate;
            EditItem.NoView = item.NoView;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        [ActionName("Edit")]
        public ActionResult EditConference(object id)
        {
            return base.Edit(id);
        }
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConference(ConferenceEditBindModel EditItem, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = Server.MapPath(Classes.Utilities.SiteConfig.UploadFilesPath);
                string fileName = EditItem.ConferenceName;
                Classes.Utilities.Utility.SaveFile(file, path, "Conference", ref fileName);
                EditItem.ImageName = fileName;
            }
            return base.Edit(EditItem);
        }

        #region Override Actions
        [NonAction]
        public override ActionResult Create()
        {
            return null;
        }
        [NonAction]
        public override ActionResult Create(ConferenceCreateBindModel model)
        {
            return base.Create(model);
        }

        [NonAction]
        public override ActionResult Edit(object id)
        {
            return base.Edit(id);
        }
        [NonAction]
        public override ActionResult Edit(ConferenceEditBindModel EditItem)
        {
            return base.Edit(EditItem);
        }
        #endregion
    }
}