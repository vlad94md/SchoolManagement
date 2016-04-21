using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Domain;

namespace SchoolManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();

        public ActionResult Login()
        {
            var gg = repository.Administrators;
            return View();
        }

    }
}
