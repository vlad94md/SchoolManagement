using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();

        public ActionResult Index()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            return View();
        }

        public ActionResult Details()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            Teacher details = repository.Teachers.FirstOrDefault(x => x.PIN == currUser.Login);

            var descipline = repository.Disciplines.FirstOrDefault(x => x.Id == details.Discipline_Id);

            if (descipline != null)
            {
                ViewBag.desciplineName = descipline.Subject;
            }

            return View(details);
        }

        public ActionResult SearchStudents()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            var thisTeacher = repository.Teachers.FirstOrDefault(x => x.PIN == currUser.Login);
            Dictionary<string, int> classNames = new Dictionary<string, int>();
            List<Student> studentsToStudy = new List<Student>();

            foreach (var item in thisTeacher.ClassesToStudy)
            {
                var curClass = repository.Classes.FirstOrDefault(x => x.Id == item.Id);
                classNames.Add(curClass.Name, curClass.Id);

                var studentsForThisClass = repository.Students.Where(x => x.Class_Id == item.Id);
                studentsToStudy.AddRange(studentsForThisClass);
            }

            TeacherSearchStudentsViewModel model = new TeacherSearchStudentsViewModel() { ClassNames = classNames, StudentsToStudy = studentsToStudy };
            return View(model);
        }

        public ActionResult ViewStudent(string id)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            var curStudent = repository.Students.FirstOrDefault(x => x.PIN == id);

            if (curStudent == null)
                return View("ViewStudentFailed");

            return View();
        }


        public ActionResult AddMark(int studentId)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            return View();
        }

        private ActionResult CheckUserRights()
        {
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            if (currUser == null || currUser.Role != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            return null;
        }

    }
}
