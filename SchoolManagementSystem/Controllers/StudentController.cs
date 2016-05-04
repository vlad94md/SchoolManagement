using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();


        public ActionResult Index()
        {
            var redirector = CheckUserRights();
            if (redirector != null)
            {
                return redirector;
            }

            return View();
        }

        public ActionResult Overview()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            var thisMonthMarks = repository.Marks.Where(x => x.Date.Month == DateTime.Now.Month 
                                                        && x.Student_PIN == currUser.Login).ToList();

            StudentMonthOverviewModel result = new StudentMonthOverviewModel()
            {
                StudentMarks = thisMonthMarks,
                Subjects = repository.Disciplines.ToList()
            };
            return View(result);
        }


        private ActionResult CheckUserRights()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            if (currUser == null || currUser.Role != "Student")
            {
                return RedirectToAction("Login", "Account");
            }

            return null;
        }

    }
}
