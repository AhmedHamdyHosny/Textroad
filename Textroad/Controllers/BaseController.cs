using Textroad.Models;
using Classes.Common;
using System;

namespace Textroad.Controllers
{
    public class BaseController<TDBModel, TViewModel, TIndexViewModel, TDetailsViewModel, TCreateBindModel, TEditBindModel, TEditModel, TImportModel, TModel_TDBModel, TModel_TViewModel> : GenericContoller<TDBModel, TViewModel, TIndexViewModel, TDetailsViewModel, TCreateBindModel, TEditBindModel, TEditModel, TImportModel, TModel_TDBModel, TModel_TViewModel>
    {
        //public UserViewModel User = new UserViewModel().GetUserFromSession();

        //for test
        //public UserViewModel User = new UserModel<UserViewModel>().Get("5C8FBE12-69B2-4EB7-B116-201AEEBD862B");
        
    }
}