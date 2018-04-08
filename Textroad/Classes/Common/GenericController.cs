using Textroad.Models;
using Classes.Utilities;
using GenericApiController.Utilities;
using Models;
using Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static Classes.Common.Enums;

namespace Classes.Common
{
    public partial class GenericContoller<TDBModel, TViewModel, TIndexViewModel, TDetailsViewModel, TCreateBindModel, TEditBindModel, TEditModel, TImportModel, TModel_TDBModel, TModel_TViewModel> : Controller
    {
        public UserViewModel User = new UserViewModel().GetUserFromSession();
        public Guid ServiceId;
        public List<UserRoleServiceAccessViewModel> UserPermission { get; set; }
        public bool GrantPermission { get; set; }
        //where TViewModel : class
        //where TIndexViewModel : class
        public List<GenericDataFormat.FilterItems> filters;
        public string ExportFileName = typeof(TDBModel).ToString() + ".xlsx";
        // GET: Controller
        public virtual ActionResult Index()
        {
            GrantPermission = UserHasPermission(DBEnums.AccessType.View);
            if (GrantPermission)
            {
                if (TempData["AlertMessage"] != null)
                {
                    ViewBag.AlertMessage = TempData["AlertMessage"];
                }
                //set page control permission
                SetIndexViewControlPermission();
                //create instance of List of TViewModel that hold data
                var model = (List<TIndexViewModel>)Activator.CreateInstance(typeof(List<TIndexViewModel>));
                DelegatePreIndexView delegatePreExecute = new DelegatePreIndexView(FuncPreIndexView);
                delegatePreExecute(ref model);
                DelegatePostIndexView delegatePostExecute = new DelegatePostIndexView(FuncPostIndexView);
                return delegatePostExecute(ref model);
            }
            else
            {
                return RedirectToAction("Partial_UnAuthorized", "Account", new { layout = Enums.Layout._Default });
            }

        }
        // GET: Controller/Details/5
        public virtual ActionResult Details(object id, int? notificationId = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ////set notification to readed
            //if (notificationId != null)
            //{
            //    NotificationEditBindModel notification = new NotificationEditBindModel();
            //    notification.UpdateIsRead(true, notificationId: notificationId);
            //}

            GrantPermission = UserHasPermission(DBEnums.AccessType.Details);
            if (GrantPermission)
            {
                //create instance List of TViewModel
                var items = (List<TDetailsViewModel>)Activator.CreateInstance(typeof(List<TDetailsViewModel>));
                DelegatePreDetailsView delegatePreExecute = new DelegatePreDetailsView(FuncPreDetailsView);
                delegatePreExecute(id, ref items);
                if (items.Count < 1 || items.ElementAt(0) == null)
                {
                    return HttpNotFound();
                }
                var model = (TDetailsViewModel)items.ElementAt(0);
                DelegatePostDetailsView delegatePostExecute = new DelegatePostDetailsView(FuncPostDetailsView);
                return delegatePostExecute(ref model);
            }
            else
            {
                return RedirectToAction("Partial_UnAuthorized", "Account");
            }

        }
        // GET: Controller/Create
        public virtual ActionResult Create()
        {
            GrantPermission = UserHasPermission(DBEnums.AccessType.Add);
            if (GrantPermission)
            {
                DelegatePreInitCreateView delegatePreExecute = new DelegatePreInitCreateView(FuncPreInitCreateView);
                delegatePreExecute();

                DelegatePostInitCreateView delegatePostExecute = new DelegatePostInitCreateView(FuncPostInitCreateView);
                return delegatePostExecute();
            }
            else
            {
                return RedirectToAction("Partial_UnAuthorized", "Account");
            }

        }
        // POST: Controller/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public virtual ActionResult Create(TCreateBindModel model)
        {
            if (ModelState.IsValid)
            {
                DelegatePreCreate delegatePreExecute = new DelegatePreCreate(FuncPreCreate);
                delegatePreExecute(ref model);
                //create instance to insert object
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                TDBModel item = instance.Insert(model);
                bool success = (item != null);

                DelegatePostCreate delegatePostExecute = new DelegatePostCreate(FuncPostCreate);
                return delegatePostExecute(ref model, ref item, false, success, null);
            }
            else
            {
                var errors = ModelState.Values.Where(i => i.Errors.Count > 0);
            }
            DelegatePreInitCreateView delegatePreExecuteInitCreateView = new DelegatePreInitCreateView(FuncPreInitCreateView);
            delegatePreExecuteInitCreateView();
            return View(model);
        }
        // GET: Controller/CreateGroup
        [HttpGet]
        [ActionName("CreateGroup")]
        public virtual ActionResult CreateGroup()
        {
            GrantPermission = UserHasPermission(DBEnums.AccessType.Add);
            if (GrantPermission)
            {
                DelegatePreInitCreateView delegatePreExecute = new DelegatePreInitCreateView(FuncPreInitCreateView);
                delegatePreExecute();

                DelegatePostInitCreateView delegatePostExecute = new DelegatePostInitCreateView(FuncPostInitCreateView);
                return delegatePostExecute();
            }
            else
            {
                return RedirectToAction("Partial_UnAuthorized", "Account");
            }

        }
        // POST: Controller/CreateGroup
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ActionName("CreateGroup")]
        public virtual ActionResult CreateGroup(TCreateBindModel[] items, FormCollection fc = null)
        {
            DelegatePreCreateGroup delegatePreExecute = new DelegatePreCreateGroup(FuncPreCreate);
            delegatePreExecute(ref items, fc);

            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            bool success = instance.Import(items);

            DelegatePostCreateGroup delegatePostExecute = new DelegatePostCreateGroup(FuncPostCreate);
            return delegatePostExecute(ref items, fc, false, success, null);
        }
        //GET: Controller/Edit/5
        public virtual ActionResult Edit(object id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //check user has permission
            GrantPermission = UserHasPermission(DBEnums.AccessType.Edit);
            if (GrantPermission)
            {
                //create instance of TDBModel
                var EditItem = default(TDBModel); //(TDBModel)Activator.CreateInstance(typeof(TDBModel));
                //create instance of TEditModel 
                var model = (TEditModel)Activator.CreateInstance(typeof(TEditModel));

                DelegatePreInitEditView delegatePreExecute = new DelegatePreInitEditView(FuncPreInitEditView);
                delegatePreExecute(id, ref EditItem, ref model);

                if (EditItem == null)
                {
                    //return HttpNotFound();
                    return RedirectToAction("Partial_UnAuthourizedViewModel", "Common");
                }
                DelegatePostInitEditView delegatePostExecute = new DelegatePostInitEditView(FuncPostInitEditView);
                return delegatePostExecute(id, ref model);
            }
            else
            {
                return RedirectToAction("Partial_UnAuthorized", "Account");
            }

        }
        // Post: Controller/Edit
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public virtual ActionResult Edit(TEditBindModel EditItem)
        {
            object id = null;

            if (ModelState.IsValid)
            {
                DelegatePreEdit delegatePreExecute = new DelegatePreEdit(FuncPreEdit);
                delegatePreExecute(ref id, ref EditItem);

                //create instance to update object
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var item = instance.Update(EditItem, id);
                bool success = (item != null);

                DelegatePostEdit delegatePostExecute = new DelegatePostEdit(FuncPostEdit);
                return delegatePostExecute(ref EditItem, false, success, null);
            }
            else
            {
                var errors = ModelState.Values.Where(i => i.Errors.Count > 0);
            }
            TDBModel editInstance = (TDBModel)Activator.CreateInstance(typeof(TDBModel));
            Utilities.Utility.CopyObject<TDBModel>(EditItem, ref editInstance);
            //create instance of TEditModel 
            var model = (TEditModel)Activator.CreateInstance(typeof(TEditModel));
            DelegatePreInitEditView delegatePreExecuteInitEditView = new DelegatePreInitEditView(FuncPreInitEditView);
            delegatePreExecuteInitEditView(id, ref editInstance, ref model);
            return View(model);
        }
        // POST: Controller/Delete
        [HttpPost, ActionName("Delete")]
        public virtual bool DeleteConfirmed(object id)
        {
            DelegatePreDelete delegatePreExecute = new DelegatePreDelete(FuncPreDelete);
            delegatePreExecute(id);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            bool success = instance.Delete(id);

            DelegatePostDelete delegatePostExecute = new DelegatePostDelete(FuncPostDelete);
            return delegatePostExecute(id, success);

        }
        // POST: Controller/Delete
        [HttpPost, ActionName("DeleteGroup")]
        public virtual bool DeleteConfirmed(object[] ids)
        {
            DelegatePreDeleteGroup delegatePreExecute = new DelegatePreDeleteGroup(FuncPreDelete);
            delegatePreExecute(ids);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            bool success = instance.Delete(ids);

            DelegatePostDeleteGroup delegatePostExecute = new DelegatePostDeleteGroup(FuncPostDelete);
            return delegatePostExecute(ids, success);
        }
        // POST: Controller/Deactive
        [HttpPost, ActionName("Deactive")]
        public virtual bool DeactiveConfirmed(object id)
        {
            DelegatePreDeactivate delegatePreExecute = new DelegatePreDeactivate(FuncPreDeactivate);
            delegatePreExecute(id);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Deactive(id);

            DelegatePostDeactivate delegatePostExecute = new DelegatePostDeactivate(FuncPostDeactivate);
            return delegatePostExecute(id);
        }
        // POST: Controller/Deactive
        [HttpPost, ActionName("DeactiveGroup")]
        public virtual bool DeactiveGroupConfirmed(object[] ids)
        {
            DelegatePreDeactivateGroup delegatePreExecute = new DelegatePreDeactivateGroup(FuncPreDeactivate);
            delegatePreExecute(ids);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Deactive(ids);

            DelegatePostDeactivateGroup delegatePostExecute = new DelegatePostDeactivateGroup(FuncPostDeactivate);
            return delegatePostExecute(ids);
        }
        // POST: Controller/Active
        [HttpPost, ActionName("Active")]
        public virtual bool ActiveConfirmed(object id)
        {
            DelegatePreActivate delegatePreExecute = new DelegatePreActivate(FuncPreActivate);
            delegatePreExecute(id);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Active(id);

            DelegatePostActivate delegatePostExecute = new DelegatePostActivate(FuncPostActivate);
            return delegatePostExecute(id);
        }
        // POST: Controller/active
        [HttpPost, ActionName("ActiveGroup")]
        public virtual bool ActiveGroupConfirmed(object[] ids)
        {
            DelegatePreActivateGroup delegatePreExecute = new DelegatePreActivateGroup(FuncPreActivate);
            delegatePreExecute(ids);

            //create instance of TModel of TViewModel
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            instance.Active(ids);

            DelegatePostActivateGroup delegatePostExecute = new DelegatePostActivateGroup(FuncPostActivate);
            return delegatePostExecute(ids);
        }
        // POST: Controller/Import
        [HttpPost]
        public virtual ActionResult Import(FormCollection fc)
        {
            try
            {
                DelegatePreImport delegatePreExecute = new DelegatePreImport(FuncPreImport);
                delegatePreExecute(fc);

                var file = Request.Files["file"];
                //create instance of TModel of TViewModel
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var result = instance.Import(file);

                DelegatePostImport delegatePostExecute = new DelegatePostImport(FuncPostImport);
                return delegatePostExecute(result);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is UnauthorizedAccessException)
                {
                    return RedirectToAction("Unauthorized", "Login");
                }

                throw ex;
            }

        }
        // POST: Controller/Export
        public virtual ActionResult Export(GenericDataFormat ExportRequestBody = null)
        {
            try
            {
                DelegatePreExport delegatePreExecute = new DelegatePreExport(FuncPreExport);
                delegatePreExecute(ref ExportRequestBody, ref this.ExportFileName);

                //create instance of TModel of TDBModel
                dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                var fileBytes = instance.Export(ExportRequestBody);

                DelegatePostExport delegatePostExecute = new DelegatePostExport(FuncPostExport);
                return delegatePostExecute(fileBytes, this.ExportFileName);

            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is UnauthorizedAccessException)
                {
                    return RedirectToAction("Unauthorized", "Login");
                }
                throw ex;
            }
        }
        //POST : Controller/GetView
        [HttpPost]
        public JsonResult GetView(GenericDataFormat options)
        {
            var model = (PaginationResult<TIndexViewModel>)Activator.CreateInstance(typeof(PaginationResult<TIndexViewModel>));
            DelegatePreGetView delegatePreExecute = new DelegatePreGetView(FuncPreGetView);
            delegatePreExecute(ref model, options);
            DelegatePostGetView delegatePostExecute = new DelegatePostGetView(FuncPostGetView);
            return delegatePostExecute(ref model);

        }
        //POST : Controller/GetGridView
        [HttpPost]
        public JsonResult GetGridView(GenericDataFormat options)
        {
            var model = (PaginationResult<TIndexViewModel>)Activator.CreateInstance(typeof(PaginationResult<TIndexViewModel>));
            DelegatePreGetGridView delegatePreExecute = new DelegatePreGetGridView(FuncPreGetGridView);
            delegatePreExecute(ref model, options);
            DelegatePostGetGridView delegatePostExecute = new DelegatePostGetGridView(FuncPostGetGridView);
            return delegatePostExecute(ref model);
        }
        // POST: Controller/ImportAsEntities
        [HttpPost]
        public virtual ActionResult ImportAsEntities(FormCollection fc)
        {
            var file = Request.Files["file"];
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            string error = "";
            string path = Server.MapPath(SiteConfig.ImportFilesPath);
            var fileName = Utilities.Utility.DataImportHelper(file, path, typeof(TDBModel).GetType().Name);
            var items = ParseExcelFile(fileName, ref error);

            DelegatePreImportAsEntities delegatePreExecute = new DelegatePreImportAsEntities(FuncPreImportAsEntities);
            delegatePreExecute(ref items, fc);

            var result = instance.Import(items.ToArray());

            DelegatePostImportAsEntities delegatePostExecute = new DelegatePostImportAsEntities(FuncPostImportAsEntities);
            return delegatePostExecute(result);

        }

        [HttpPost]
        public virtual ActionResult ImportAsEntitiesToUpdate(FormCollection fc)
        {
            var file = Request.Files["file"];
            dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
            string error = "";
            string path = Server.MapPath(SiteConfig.ImportFilesPath);
            var fileName = Utilities.Utility.DataImportHelper(file, path, typeof(TDBModel).Name);
            var items = ParseExcelFile(fileName, ref error);

            List<UpdateItemFormat<TDBModel>> newItems = new List<UpdateItemFormat<TDBModel>>();

            DelegatePreImportAsEntitiesToUpdate delegatePreExecute = FuncPreImportAsEntitiesToUpdate;
            delegatePreExecute(ref items, ref newItems, fc);

            var updateResult = instance.Update(newItems);
            var result = updateResult.Count > 0;

            DelegatePostImportAsEntitiesToUpdate delegatePostExecute = FuncPostImportAsEntitiesToUpdate;
            return delegatePostExecute(result, updateResult);

        }
        [NonAction]
        public IEnumerable<TImportModel> ParseExcelFile(string fileName, ref string errorMsg)
        {
            var excelFile = new LinqToExcel.ExcelQueryFactory(fileName);
            IEnumerable<string> workSheetNames = excelFile.GetWorksheetNames();
            IEnumerable<TImportModel> sheetData = new List<TImportModel>();
            foreach (string sheetName in workSheetNames)
            {
                List<string> excelHeaders = excelFile.GetColumnNames(sheetName).ToList();
                List<string> props = typeof(TImportModel).GetProperties().Select(x => x.Name).ToList();
                var diff = excelHeaders.Except(props);
                bool isSubset = !excelHeaders.Except(props).Any();
                if (isSubset)
                {
                    errorMsg = "";
                    var data = from row in excelFile.Worksheet<TImportModel>(sheetName)
                               select row;
                    sheetData = data.AsEnumerable();
                }
            }
            return sheetData;
        }
        [NonAction]
        public IEnumerable<TModel> ParseExcelFile<TModel>(string fileName, ref string errorMsg)
        {
            var excelFile = new LinqToExcel.ExcelQueryFactory(fileName);
            IEnumerable<string> workSheetNames = excelFile.GetWorksheetNames();
            IEnumerable<TModel> sheetData = new List<TModel>();
            foreach (string sheetName in workSheetNames)
            {
                List<string> excelHeaders = excelFile.GetColumnNames(sheetName).ToList();
                List<string> props = typeof(TModel).GetProperties().Select(x => x.Name).ToList();
                bool isSubset = !excelHeaders.Except(props).Any();
                if (isSubset)
                {
                    errorMsg = "";
                    var data = from row in excelFile.Worksheet<TModel>(sheetName)
                               select row;
                    sheetData = data.AsEnumerable();
                }
            }
            return sheetData;
        }
        // POST: Controller/getinfo
        [HttpPost]
        public JsonResult GetInfo(GenericDataFormat Options)
        {
            //create instance List of TViewModel
            var model = (List<TViewModel>)Activator.CreateInstance(typeof(List<TViewModel>));
            if (Options.Filters != null)
            {
                dynamic instance = Activator.CreateInstance(typeof(TModel_TViewModel));
                model = instance.GetView<TViewModel>(Options).PageItems;
            }
            return Json(model);
        }
        // POST: Controller/getddl
        [HttpPost]
        public JsonResult GetDDL(GenericDataFormat Options, bool fromView = false)
        {
            var filters = Options.Filters;
            IEnumerable<Helper.CustomSelectListItem> model = null;
            if (fromView)
            {
                if (Options.Filters != null)
                {
                    dynamic instance = Activator.CreateInstance(typeof(TModel_TViewModel));
                    model = instance.GetDDL(filters: filters);
                }
            }
            else
            {
                if (Options.Filters != null)
                {
                    dynamic instance = Activator.CreateInstance(typeof(TModel_TDBModel));
                    model = instance.GetDDL(filters: filters);
                }
            }
            return Json(model.ToList());
        }
        [NonAction]
        public bool UserHasPermission(Guid accessType)
        {
            if (UserPermission == null)
            {
                UserPermission = new UserRoleServiceAccessModel<UserRoleServiceAccessViewModel>().GetSavedUserPermission();
            }

            bool grantPermission = false;
            if (User.UserTypeID == DBEnums.UserType.Admin)
            {
                grantPermission = true;
            }
            else
            {
                grantPermission = UserPermission.Any(x => x.ServiceId == ServiceId && x.AccessTypeId == accessType);
            }
            return grantPermission;
        }

        private void SetIndexViewControlPermission()
        {
            GenericAccessPermission permssion = new GenericAccessPermission();
            if (User.UserTypeID == DBEnums.UserType.Admin)
            {
                foreach (var prop in typeof(GenericAccessPermission).GetProperties())
                {
                    Utilities.Utility.SetPropertyValue(ref permssion, prop.Name, true);
                }
            }
            else
            {
                //get user permission
                var UserPermissions = new UserRoleServiceAccessModel<UserRoleServiceAccessViewModel>().GetUserPermissiom(User.UserId, true).ToList();
                permssion.ViewPermission = UserPermissions.Any(x => x.ServiceId == ServiceId && x.AccessTypeId == DBEnums.AccessType.View);
                permssion.AddPermission = UserPermissions.Any(x => x.ServiceId == ServiceId && x.AccessTypeId == DBEnums.AccessType.Add);
                permssion.EditPermission = UserPermissions.Any(x => x.ServiceId == ServiceId && x.AccessTypeId == DBEnums.AccessType.Edit);
                permssion.DetailsPermission = UserPermissions.Any(x => x.ServiceId == ServiceId && x.AccessTypeId == DBEnums.AccessType.Details);
                permssion.DeletePermission = UserPermissions.Any(x => x.ServiceId == ServiceId && x.AccessTypeId == DBEnums.AccessType.Delete);
                permssion.ExportPermission = UserPermissions.Any(x => x.ServiceId == ServiceId && x.AccessTypeId == DBEnums.AccessType.Export);
                permssion.ImportPermission = UserPermissions.Any(x => x.ServiceId == ServiceId && x.AccessTypeId == DBEnums.AccessType.Import);
            }
            ViewBag.Permission = permssion;
        }
    }

    public partial class GenericContoller<TDBModel, TViewModel, TIndexViewModel, TDetailsViewModel, TCreateBindModel, TEditBindModel, TEditModel, TImportModel, TModel_TDBModel, TModel_TViewModel> : Controller
    {
        #region Delegates
        public delegate void DelegatePreIndexView(ref List<TIndexViewModel> model);
        public delegate ActionResult DelegatePostIndexView(ref List<TIndexViewModel> model);
        public delegate void DelegatePreGetView(ref PaginationResult<TIndexViewModel> model, GenericDataFormat options);
        public delegate JsonResult DelegatePostGetView(ref PaginationResult<TIndexViewModel> model);
        public delegate void DelegatePreGetGridView(ref PaginationResult<TIndexViewModel> model, GenericDataFormat options);
        public delegate JsonResult DelegatePostGetGridView(ref PaginationResult<TIndexViewModel> model);
        public delegate void DelegatePreDetailsView(object id, ref List<TDetailsViewModel> items);
        public delegate ActionResult DelegatePostDetailsView(ref TDetailsViewModel model);
        public delegate void DelegatePreInitCreateView();
        public delegate ActionResult DelegatePostInitCreateView();
        public delegate void DelegatePreCreate(ref TCreateBindModel model);
        public delegate ActionResult DelegatePostCreate(ref TCreateBindModel model, ref TDBModel insertedItem, bool UseAjaxCall = false, bool Success = true, string Message = null);
        public delegate void DelegatePreCreateGroup(ref TCreateBindModel[] items, FormCollection formCollection);
        public delegate ActionResult DelegatePostCreateGroup(ref TCreateBindModel[] items, FormCollection formCollection, bool UseAjaxCall = false, bool Success = true, string Message = null);
        public delegate void DelegatePreInitEditView(object id, ref TDBModel EditItem, ref TEditModel model);
        public delegate ActionResult DelegatePostInitEditView(object id, ref TEditModel model);
        public delegate void DelegatePreEdit(ref object id, ref TEditBindModel EditItem);
        public delegate ActionResult DelegatePostEdit(ref TEditBindModel EditItem, bool UseAjaxCall = false, bool Success = true, string Message = null);
        public delegate void DelegatePreDelete(object id);
        public delegate bool DelegatePostDelete(object id, bool success);
        public delegate void DelegatePreDeleteGroup(object[] ids);
        public delegate bool DelegatePostDeleteGroup(object[] ids, bool success);
        public delegate void DelegatePreDeactivate(object id);
        public delegate bool DelegatePostDeactivate(object id);
        public delegate void DelegatePreDeactivateGroup(object[] ids);
        public delegate bool DelegatePostDeactivateGroup(object[] ids);
        public delegate void DelegatePreActivate(object id);
        public delegate bool DelegatePostActivate(object id);
        public delegate void DelegatePreActivateGroup(object[] ids);
        public delegate bool DelegatePostActivateGroup(object[] ids);
        public delegate void DelegatePreImport(FormCollection formCollection);
        public delegate ActionResult DelegatePostImport(bool result);
        public delegate void DelegatePreImportAsEntities(ref IEnumerable<TImportModel> items, FormCollection formCollection);
        public delegate ActionResult DelegatePostImportAsEntities(bool result);
        public delegate void DelegatePreImportAsEntitiesToUpdate(ref IEnumerable<TImportModel> items, ref List<UpdateItemFormat<TDBModel>> updateItem, FormCollection formCollection);
        public delegate ActionResult DelegatePostImportAsEntitiesToUpdate(bool result, List<TDBModel> updatedItems);
        public delegate void DelegatePreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName);
        public delegate ActionResult DelegatePostExport(byte[] fileBytes, string ExportFileName);
        #endregion

        #region Delegate Functions
        public virtual void FuncPreIndexView(ref List<TIndexViewModel> model)
        {
        }
        public virtual ActionResult FuncPostIndexView(ref List<TIndexViewModel> model)
        {
            return View(model);
        }
        public virtual void FuncPreGetView(ref PaginationResult<TIndexViewModel> model, GenericDataFormat Options)
        {
            if (Options.Sorts != null && Options.Sorts.Count > 1)
            {
                Options.Sorts = Options.Sorts.Where(x => x.Priority == 1).ToList();
            }
            dynamic instance = Activator.CreateInstance(typeof(TModel_TViewModel));
            model = instance.GetView<TIndexViewModel>(Options);
        }
        public virtual JsonResult FuncPostGetView(ref PaginationResult<TIndexViewModel> model)
        {
            return Json(model);
        }
        public virtual void FuncPreGetGridView(ref PaginationResult<TIndexViewModel> model, GenericDataFormat Options)
        {
            if (Options.Sorts != null && Options.Sorts.Count > 1)
            {
                Options.Sorts = Options.Sorts.Where(x => x.Priority == 1).ToList();
            }
            dynamic instance = Activator.CreateInstance(typeof(TModel_TViewModel));
            model = instance.GetGridView<TIndexViewModel>(Options);
        }
        public virtual JsonResult FuncPostGetGridView(ref PaginationResult<TIndexViewModel> model)
        {
            return Json(model);
        }
        public virtual void FuncPreDetailsView(object id, ref List<TDetailsViewModel> items)
        {
        }
        public virtual ActionResult FuncPostDetailsView(ref TDetailsViewModel model)
        {
            return View(model);
        }

        #region Create Functions


        public virtual void FuncPreInitCreateView()
        {
        }
        public virtual ActionResult FuncPostInitCreateView()
        {
            return View();
        }
        public virtual void FuncPreCreate(ref TCreateBindModel model)
        {

        }
        public virtual ActionResult FuncPostCreate(ref TCreateBindModel model, ref TDBModel insertedItem, bool UseAjaxCall = false, bool Success = true, string Message = null)
        {
            if (UseAjaxCall)
            {
                JsonResponse result = new JsonResponse();
                if (Success)
                {
                    //set alerts messages
                    AlertMessage alertMsg = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Create };
                    if (!string.IsNullOrEmpty(Message))
                    {
                        alertMsg.MessageContent = Message;
                    }
                    TempData["AlertMessage"] = alertMsg;
                    result.Status = 1;
                }
                else
                {
                    AlertMessage alertMsg = new AlertMessage() { MessageType = AlertMessageType.Error, TransactionCount = 1, Transaction = Transactions.Create };
                    if (!string.IsNullOrEmpty(Message))
                    {
                        alertMsg.MessageContent = Message;
                    }
                    TempData["AlertMessage"] = alertMsg;
                    result.Status = 0;
                }
                //go to index after insert action
                result.RedirectTo = "Index";
                return Json(result);
            }
            else
            {
                //set alerts messages
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Create };
                //go to after insert action
                return RedirectToAction("Index");
            }
        }
        public virtual void FuncPreCreate(ref TCreateBindModel[] items, FormCollection formCollection)
        {
        }
        public virtual ActionResult FuncPostCreate(ref TCreateBindModel[] items, FormCollection formCollection, bool UseAjaxCall = false, bool Success = true, string Message = null)
        {
            if (UseAjaxCall)
            {
                JsonResponse result = new JsonResponse();
                if (Success)
                {
                    //set alerts messages
                    AlertMessage alertMsg = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = items.Length, Transaction = Transactions.Create };
                    if (!string.IsNullOrEmpty(Message))
                    {
                        alertMsg.MessageContent = Message;
                    }
                    TempData["AlertMessage"] = alertMsg;
                    result.Status = 1;
                }
                else
                {
                    AlertMessage alertMsg = new AlertMessage() { MessageType = AlertMessageType.Error, TransactionCount = items.Length, Transaction = Transactions.Create };
                    if (!string.IsNullOrEmpty(Message))
                    {
                        alertMsg.MessageContent = Message;
                    }
                    TempData["AlertMessage"] = alertMsg;
                    result.Status = 0;
                }
                //go to index after insert action
                result.RedirectTo = "Index";
                return Json(result);
            }
            else
            {
                //set alerts messages
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = items.Length, Transaction = Transactions.Create };
                return RedirectToAction("Index");
            }
        }



        #endregion

        #region Edit Functions


        public virtual void FuncPreInitEditView(object id, ref TDBModel EditItem, ref TEditModel model)
        {
        }
        public virtual ActionResult FuncPostInitEditView(object id, ref TEditModel model)
        {
            return View(model);
        }
        public virtual void FuncPreEdit(ref object id, ref TEditBindModel EditItem)
        {
        }
        public virtual ActionResult FuncPostEdit(ref TEditBindModel EditItem, bool UseAjaxCall = false, bool Success = true, string Message = null)
        {
            if (UseAjaxCall)
            {
                JsonResponse result = new JsonResponse();
                if (Success)
                {
                    //set alerts messages
                    AlertMessage alertMsg = new AlertMessage() { MessageType = Enums.AlertMessageType.Success, TransactionCount = 1, Transaction = Enums.Transactions.Edit };
                    if (!string.IsNullOrEmpty(Message))
                    {
                        alertMsg.MessageContent = Message;
                    }
                    TempData["AlertMessage"] = alertMsg;
                    result.Status = 1;
                }
                else
                {
                    AlertMessage alertMsg = new AlertMessage() { MessageType = Enums.AlertMessageType.Error, TransactionCount = 1, Transaction = Enums.Transactions.Edit };
                    if (!string.IsNullOrEmpty(Message))
                    {
                        alertMsg.MessageContent = Message;
                    }
                    TempData["AlertMessage"] = alertMsg;
                    result.Status = 0;
                }
                //go to index after insert action
                result.RedirectTo = "Index";
                return Json(result);
            }
            else
            {
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Edit };
                return RedirectToAction("Index");
            }
        }


        #endregion

        #region Delete Functions


        public virtual void FuncPreDelete(object id)
        {

        }
        public virtual bool FuncPostDelete(object id, bool success)
        {
            if (success)
            {
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Delete };
            }
            else
            {
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Error, TransactionCount = 1, Transaction = Transactions.Delete };
            }
            return true;
        }
        public virtual void FuncPreDelete(object[] ids)
        {

        }
        public virtual bool FuncPostDelete(object[] ids, bool success)
        {
            if (success)
            {
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Delete };
            }
            else
            {
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Error, TransactionCount = ids.Count(), Transaction = Transactions.Delete };
            }

            return true;
        }


        #endregion


        public virtual void FuncPreDeactivate(object id)
        {

        }
        public virtual bool FuncPostDeactivate(object id)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Deactive };
            return true;
        }
        public virtual void FuncPreDeactivate(object[] ids)
        {

        }
        public virtual bool FuncPostDeactivate(object[] ids)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Deactive };
            return true;
        }
        public virtual void FuncPreActivate(object id)
        {

        }
        public virtual bool FuncPostActivate(object id)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = 1, Transaction = Transactions.Active };
            return true;
        }
        public virtual void FuncPreActivate(object[] ids)
        {

        }
        public virtual bool FuncPostActivate(object[] ids)
        {
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, TransactionCount = ids.Count(), Transaction = Transactions.Active };
            return true;
        }
        public virtual void FuncPreImport(FormCollection formCollection)
        {

        }
        public virtual ActionResult FuncPostImport(bool result)
        {
            if (result)
            {
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, Transaction = Transactions.Import };
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Error, Transaction = Transactions.Import };
            return RedirectToAction("Index");
        }
        public virtual void FuncPreImportAsEntities(ref IEnumerable<TImportModel> items, FormCollection formCollection)
        {

        }
        public virtual void FuncPreImportAsEntitiesToUpdate(ref IEnumerable<TImportModel> items, ref List<UpdateItemFormat<TDBModel>> updateItem, FormCollection formCollection)
        {

        }
        public virtual ActionResult FuncPostImportAsEntities(bool result)
        {
            if (result)
            {
                TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Success, Transaction = Transactions.Import };
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Error, Transaction = Transactions.Import };
            return RedirectToAction("Index");
        }

        public virtual ActionResult FuncPostImportAsEntitiesToUpdate(bool result, List<TDBModel> updatedItems)
        {
            if (result)
            {
                TempData["AlertMessage"] = new AlertMessage() { TransactionCount = updatedItems.Count, MessageType = AlertMessageType.Success, Transaction = Transactions.Edit };
                return RedirectToAction("Index");
            }
            TempData["AlertMessage"] = new AlertMessage() { MessageType = AlertMessageType.Error, Transaction = Transactions.Edit };
            return RedirectToAction("Index");
        }
        public virtual void FuncPreExport(ref GenericDataFormat ExportRequestBody, ref string ExportFileName)
        {

        }
        public virtual ActionResult FuncPostExport(byte[] fileBytes, string ExportFileName)
        {
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, ExportFileName);
        }
        #endregion
    }
}


//[NonAction]
//public bool UserHasPermission(Guid accessType)
//{
//    if (UserPermission == null)
//    {
//        UserPermission = new UserRoleServiceAccessModel<UserRoleServiceAccessViewModel>().GetSavedUserPermission();
//    }
//    bool grantPermission = false;
//    if (User.UserTypeID == DBEnums.UserType.Admin)
//    {
//        grantPermission = true;
//    }
//    else
//    {
//        grantPermission = UserPermission.Any(x => x.ServiceId == ServiceId && x.AccessTypeId == accessType);
//    }
//    return grantPermission;
//}

