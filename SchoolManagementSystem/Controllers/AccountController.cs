using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();

        public ActionResult Login()
        {
            repository.context.Administrators.Add(new Administrator {FirstName = "gg", Login = "admin"});
            return View();
        }

    }
}
