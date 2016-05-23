using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
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
            List<StudentViewModel> model = new List<StudentViewModel>();

            foreach (var item in students)
            {
                var studentClass = repository.Classes.FirstOrDefault(x => x.Id == item.Class_Id);

                model.Add(new StudentViewModel() { Student = item, ClassName = studentClass.Name });
            }

            return View(model);
        }

        public ActionResult Teachers()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var teachers = repository.Teachers.ToList();
            List<TeacherViewModel> model = new List<TeacherViewModel>();

            foreach (var item in teachers)
            {
                var teacherDiscipline = repository.Disciplines.FirstOrDefault(x => x.Id == item.Discipline_Id);

                model.Add(new TeacherViewModel() { Teacher = item, Discipline = teacherDiscipline.Subject });
            }

            return View(model);
        }

        public ActionResult AddStudent()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            List<Class> model = repository.Classes.ToList();

            return View(model);
        }

        public ActionResult AddTeacher()
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            SecretaryAddTeacherViewModel model = new SecretaryAddTeacherViewModel();

            model.Disciplines = repository.Disciplines.ToList();
            model.Classes = repository.Classes.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var curStud = repository.Students.FirstOrDefault(x => x.PIN == student.PIN);

            if (curStud != null)
            {
                ViewBag.message = "Student was not created! Such PIN already exists";
                List<Class> mod = repository.Classes.ToList();

                return View(mod);
            }

            repository.AddStudent(student);

            ViewBag.message = "A new student was successfully added.";
            List<Class> model = repository.Classes.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddTeacher(Teacher teacher, int[] classes)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            SecretaryAddTeacherViewModel model = new SecretaryAddTeacherViewModel();

            model.Disciplines = repository.Disciplines.ToList();
            model.Classes = repository.Classes.ToList();


            var curTeach = repository.Teachers.FirstOrDefault(x => x.PIN == teacher.PIN);

            if (curTeach != null)
            {
                ViewBag.message = "Teacher was not created! Such PIN already exists";
                List<Class> mod = repository.Classes.ToList();

                return View(model);
            }

            List<Class> classteToStudy = new List<Class>();
            foreach (var classItem in classes)
            {
                var classToStudy = repository.Classes.FirstOrDefault(x => x.Id == classItem);
                classteToStudy.Add(classToStudy);
            }

            teacher.ClassesToStudy = classteToStudy;
            repository.AddTeacher(teacher);

            ViewBag.message = "A new teacher was successfully added.";

            return View(model);
        }

        public ActionResult Search(string search)
        {
            var redirector = CheckUserRights();
            if (redirector != null) return redirector;

            var searchLowCase = search.ToLower();

            var students = repository.Students.Where(
                x => x.FirstName.ToLower().Contains(searchLowCase) ||
                x.LastName.ToLower().Contains(searchLowCase) ||
                x.PIN == searchLowCase).ToList();

            List<StudentViewModel> modelStud = new List<StudentViewModel>();

            foreach (var item in students)
            {
                var studentClass = repository.Classes.FirstOrDefault(x => x.Id == item.Class_Id);

                modelStud.Add(new StudentViewModel() { Student = item, ClassName = studentClass.Name });
            }

            var teachers = repository.Teachers.Where(
                x => x.FirstName.ToLower().Contains(searchLowCase) ||
                x.LastName.ToLower().Contains(searchLowCase) ||
                x.PIN == searchLowCase).ToList();

            List<TeacherViewModel> modelTeach = new List<TeacherViewModel>();

            foreach (var item in teachers)
            {
                var teacherDiscipline = repository.Disciplines.FirstOrDefault(x => x.Id == item.Discipline_Id);

                modelTeach.Add(new TeacherViewModel() { Teacher = item, Discipline = teacherDiscipline.Subject });
            }

            return View(new SecretarySearchModel() { Students = modelStud, Teachers = modelTeach });
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
