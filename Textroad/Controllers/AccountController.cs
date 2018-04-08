using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Textroad.Models;

namespace Textroad.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account/login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = model.Login();
                if (user != null)
                {
                    user.SaveUserToLocalStorage(model.RememberMe);
                    return RedirectToAction("Index", "Scope");
                }
                else
                {
                    ViewBag.Error = Resources.Resource.AlertLoginFailedErrorMessage;
                }
            }
            return View();
        }
    }
}