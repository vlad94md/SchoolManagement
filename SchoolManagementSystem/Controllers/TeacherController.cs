using System;
using System.Collections.Generic;
using System.Globalization;
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

        public ActionResult ViewStudentMarks(string id, int year, int month)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            ViewBag.id = id;

            var curStudent = repository.Students.FirstOrDefault(x => x.PIN == id);
            if (curStudent == null)
                return View("ViewStudentFailed");

            var thisMonthMarks = repository.Marks.Where(x => x.Date.Month == month
                                                        && x.Date.Year == year
                                                        && x.Student_PIN == curStudent.PIN).ToList();

            StudentMonthOverviewModel model = new StudentMonthOverviewModel()
            {
                StudentMarks = thisMonthMarks,
                Subjects = repository.Disciplines.ToList(),
                Month = month,
                Year = year
            };

            ViewBag.name = curStudent.FirstName + " " + curStudent.LastName;
            return View(model);
        }

        public ActionResult ViewStudent(string id)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            ViewBag.id = id;

            var curStudent = repository.Students.FirstOrDefault(x => x.PIN == id);
            if (curStudent == null)
                return View("ViewStudentFailed");

            ViewBag.name = curStudent.FirstName + " " + curStudent.LastName;
            return View();
        }

        public ActionResult AddMark(string studentId)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            var currTeacher = repository.Teachers.FirstOrDefault(x => x.PIN == currUser.Login);
            var disciplineName = repository.Disciplines.FirstOrDefault(x => x.Id == currTeacher.Discipline_Id);
            var student = repository.Students.FirstOrDefault(x => x.PIN == studentId);

            if (student != null)
            {
                ViewBag.id = studentId;
                ViewBag.name = student.FirstName + " " + student.LastName;
                ViewBag.discipline = disciplineName.Subject;
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddMark(string id, string date, string mark)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            int month = Int32.Parse(date.Substring(0, 2));
            int days = Int32.Parse(date.Substring(3, 2));
            int year = Int32.Parse(date.Substring(6, 4));
            DateTime actualDate = new DateTime(year, month, days);
            
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];
            var currTeacher = repository.Teachers.FirstOrDefault(x => x.PIN == currUser.Login);
            var disciplineName = repository.Disciplines.FirstOrDefault(x => x.Id == currTeacher.Discipline_Id);
            var studnet = repository.Students.FirstOrDefault(x => x.PIN == id);

            if (studnet != null)
            {
                ViewBag.id = id;
                ViewBag.name = studnet.FirstName + " " + studnet.LastName;
                ViewBag.discipline = disciplineName.Subject;
            }

            var markExists = repository.Marks.FirstOrDefault(
                     x => x.Date.Date == actualDate.Date &&
                     x.Discipline_Id == currTeacher.Discipline_Id &&
                     x.Student_PIN == id);

            if (markExists != null)
            {
                ViewBag.error = "You cant put a mark on the same date and subject twice.";
                return View();
            }

            Mark markModel = new Mark()
            {
                Value = Int32.Parse(mark),
                Date = actualDate,
                Discipline_Id = currTeacher.Discipline_Id,
                Student_PIN = id,
                Teacher_PIN = currTeacher.PIN
            };

            repository.AddMark(markModel);

            ViewBag.success = "Mark was successfully added!";
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
