using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class SecretaryController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();

        public ActionResult Index()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            return View();
        }


        public ActionResult Students()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var students = repository.Students.ToList();

            return View(students);
        }

        public ActionResult Teachers()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var teachers = repository.Teachers.ToList();

            return View(teachers);
        }

        public ActionResult AddStudent()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            return View();
        }

        public ActionResult AddTeacher()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            return View();
        }

        public ActionResult Search(string search)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            return View();
        }

        private ActionResult CheckUserRights()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            if (currUser == null || currUser.Role != "Secretary")
            {
                return RedirectToAction("Login", "Account");
            }

            return null;
        }

    }
}
