using Classes.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textroad.DataLayer;
using Textroad.Models;

namespace Textroad.Controllers
{
    public class JournalController : BaseController<Journal,JournalViewModel,JournalIndexViewModel,JournalDetailsViewModel,JournalCreateBindModel, JournalEditBindModel, JournalEditModel, Journal, JournalModel<Journal>,JournalModel<JournalViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<JournalDetailsViewModel> items)
        {
            items = new JournalModel<JournalDetailsViewModel>().GetData(JournalID: id, fromView: true).ToList();
        }
        public override void FuncPreInitCreateView()
        {
            base.FuncPreInitCreateView();
            var subjects = new SubjectModel<Subject>().GetData(isBlock:false);
            ViewBag.SubjectID = subjects.Select(x => new CustomSelectListItem() { Text = x.SubjectName, Value = x.SubjectID.ToString() }).ToList();
            var publishPeriod = new PublishPeriodModel<PublishPeriod>().GetData(isBlock: false);
            ViewBag.PublishPeriodID = publishPeriod.Select(x => new CustomSelectListItem() { Text = x.PublishPeriodName, Value = x.PublishPeriodID.ToString() }).ToList();
        }
        public override void FuncPreCreate(ref JournalCreateBindModel model)
        {
            model.JournalID = Guid.NewGuid();
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref Journal EditItem, ref JournalEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new JournalModel<Journal>().Get(id);
            }
            if (EditItem != null)
            {
                model = new JournalEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                var subjects = new SubjectModel<Subject>().GetData();
                ViewBag.SubjectID = subjects.Select(x=> new CustomSelectListItem() { Text = x.SubjectName, Value = x.SubjectID.ToString(), Selected = (x.SubjectID == selectedItem.SubjectID)}).ToList();
                var publishPeriod = new PublishPeriodModel<PublishPeriod>().GetData();
                ViewBag.PublishPeriodID = publishPeriod.Select(x => new CustomSelectListItem() { Text = x.PublishPeriodName, Value = x.PublishPeriodID.ToString(), Selected = (x.PublishPeriodID == selectedItem.PublishPeriodID) }).ToList();
            }
        }
        public override void FuncPreEdit(ref object id, ref JournalEditBindModel EditItem)
        {
            id = EditItem.JournalID;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
        
    }
}