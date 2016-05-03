using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class DirectorController : Controller
    {
        //
        // GET: /Director/

        public ActionResult Index()
        {
            var redirector = CheckUserRights();
            if (redirector != null)
            {
                return redirector;
            }

            return View();
        }

        private ActionResult CheckUserRights()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            if (currUser == null || currUser.Role != "Director")
            {
                return RedirectToAction("Login", "Account");
            }

            return null;
        }

    }
}
