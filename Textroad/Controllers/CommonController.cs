using Classes.Common;
using Classes.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textroad.Models;

namespace Textroad.Controllers
{
    public class CommonController : Controller
    {
        public PartialViewResult Partial_DropDownList(string id, string name,IEnumerable<SelectListItem> list,string placeHolder, bool multipleSelect = false , bool readOnly=false)
        {
            //DropDownList list = new DropDownList();
            return PartialView();
        }

        public PartialViewResult Partial_Paging()
        {
            return PartialView();
        }

        public PartialViewResult Partial_Grid(string GridId = "grid", string UiGrid = "gridOptions", bool RowSelection = true, bool EnablePagination = true, bool GridEdit = false)
        {
            ViewBag.GridId = GridId;
            ViewBag.UiGrid = UiGrid;
            ViewBag.RowSelection = RowSelection;
            ViewBag.EnablePagination = EnablePagination;
            ViewBag.GridEdit = GridEdit;
            return PartialView();
        }

        public PartialViewResult Partial_SubjectJournalJournalVersion(PartialParamters.Subject subject = null, PartialParamters.Journal journal = null, PartialParamters.JournalVersion journalVersion = null)
        {
            //prepare dropdown list for item references
            if (subject != null && subject.Show == true && subject.Items == null)
            {
                //get all subjects
                IEnumerable<SubjectViewModel> subjects = new SubjectModel<SubjectViewModel>().GetData();
                subject.Items = subjects.Select(x => new CustomSelectListItem() { Text = x.SubjectName, Value = x.SubjectID.ToString(), Selected = (subject.SelectedItem == x.SubjectID.ToString()) }).OrderBy(x=>x.Text);
                if (subject.UseSelect)
                {
                    subject.SelectedItem = null;
                }
                else if (string.IsNullOrEmpty(subject.SelectedItem))
                {
                    subject.SelectedItem = subject.Items.ElementAt(0).Value;
                    subject.Items.ElementAt(0).Selected = true;
                }
            }

            if (journal != null && journal.Show == true && journal.Items == null)
            {
                //get all journals
                IEnumerable<JournalViewModel> journals = new JournalModel<JournalViewModel>().Get();
                journal.Items = journals.Select(x => new CustomSelectListItem() { Text = x.JournalName, Value = x.JournalID.ToString(), Group = new SelectListGroup() { Name = x.SubjectID.ToString() }, Selected = (journal.SelectedItem == x.JournalID.ToString()) }).OrderBy(x => x.Text);
                if (journal.UseSelect)
                {
                    journal.SelectedItem = null;
                }
                else if (string.IsNullOrEmpty(journal.SelectedItem))
                {
                    journal.SelectedItem = journal.Items.ElementAt(0).Value;
                    journal.Items.ElementAt(0).Selected = true;
                }
            }

            if (journalVersion != null && journalVersion.Show == true && journalVersion.Items == null)
            {
                //get all journal Versions
                var journalVersions = new JournalVersionModel<JournalVersionViewModel>().GetData().OrderByDescending(x=>x.IssueDate);
                journalVersion.Items = journalVersions.Select(x => new CustomSelectListItem() { Text = x.VersionNumber.ToString(), Value = x.JournalVersionID.ToString(), Group = new SelectListGroup() { Name = x.JournalID.ToString() }, Selected = (journalVersion.SelectedItem == x.JournalVersionID.ToString()) });
                if (journalVersion.UseSelect)
                {
                    journalVersion.SelectedItem = null;
                }
                else if (string.IsNullOrEmpty(journalVersion.SelectedItem))
                {
                    journalVersion.SelectedItem = journalVersion.Items.ElementAt(0).Value;
                    journalVersion.Items.ElementAt(0).Selected = true;
                }
            }

            ViewBag.Subject = subject;
            ViewBag.Journal = journal;
            ViewBag.JournalVersion = journalVersion;
            return PartialView();
        }
    }
}