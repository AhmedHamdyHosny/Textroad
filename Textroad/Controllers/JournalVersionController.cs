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
    public class JournalVersionController : BaseController<JournalVersion, JournalVersionViewModel, JournalVersionIndexViewModel, JournalVersionDetailsViewModel, JournalVersionCreateBindModel, JournalVersionEditBindModel, JournalVersionEditModel, JournalVersion, JournalVersionModel<JournalVersion>, JournalVersionModel<JournalVersionViewModel>>
    {
        public override void FuncPreDetailsView(object id, ref List<JournalVersionDetailsViewModel> items)
        {
            items = new JournalVersionModel<JournalVersionDetailsViewModel>().GetData(JournalVersionID: id, fromView: true).ToList();
        }
        public override void FuncPreInitCreateView()
        {
            base.FuncPreInitCreateView();
            var journals = new JournalModel<Journal>().GetData(isBlock: false);
            ViewBag.JournalID = journals.Select(x => new CustomSelectListItem() { Text = x.JournalName, Value = x.JournalID.ToString() }).ToList();
            var journalVolumeType = new JournalVolumeTypeModel<JournalVolumeType>().GetData(isBlock: false);
            ViewBag.JournalVolumeTypeID = journalVolumeType.Select(x => new CustomSelectListItem() { Text = x.JournalVolumeTypeName, Value = x.JournalVolumeTypeID.ToString() }).ToList();
        }
        public override void FuncPreCreate(ref JournalVersionCreateBindModel model)
        {
            model.JournalVersionID = Guid.NewGuid();
            model.CreateUserId = User.UserId;
            model.CreateDate = DateTime.Now;
        }
        public override void FuncPreInitEditView(object id, ref JournalVersion EditItem, ref JournalVersionEditModel model)
        {
            if (EditItem == null)
            {
                //get the item by id
                EditItem = new JournalVersionModel<JournalVersion>().Get(id);
            }
            if (EditItem != null)
            {
                model = new JournalVersionEditModel();
                model.EditItem = EditItem;
                var selectedItem = EditItem;
                var journals = new JournalModel<Journal>().GetData();
                ViewBag.JournalID = journals.Select(x => new CustomSelectListItem() { Text = x.JournalName, Value = x.JournalID.ToString(), Selected = (x.JournalID == selectedItem.JournalID)}).ToList();
                var journalVolumeType = new JournalVolumeTypeModel<JournalVolumeType>().GetData();
                ViewBag.JournalVolumeTypeID = journalVolumeType.Select(x => new CustomSelectListItem() { Text = x.JournalVolumeTypeName, Value = x.JournalVolumeTypeID.ToString(), Selected = (x.JournalVolumeTypeID == selectedItem.JournalVolumeTypeID) }).ToList();
            }
        }
        public override void FuncPreEdit(ref object id, ref JournalVersionEditBindModel EditItem)
        {
            id = EditItem.JournalVersionID;
            EditItem.ModifyUserId = User.UserId;
            EditItem.ModifyDate = DateTime.Now;
        }
    }
}