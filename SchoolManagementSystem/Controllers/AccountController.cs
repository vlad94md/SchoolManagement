using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagementSystem.Domain;
using SchoolManagementSystem.Domain.Entities;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();

        [HttpGet]
        public ActionResult Login()
        {
            // Redirects to right controller if user are logged into system 
            var currUser = (UserModel)System.Web.HttpContext.Current.Session["user"];

            if (currUser != null)
            {
                System.Web.HttpContext.Current.Session["user"] = currUser;
                return SetupRightsForUserRole(currUser);
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            // Redirects to right controller after loggin into system
            var currUser = VerifyUser(login, password);

            if (currUser == null)
            {
                return RedirectToAction("LoginFailed");
            }

            System.Web.HttpContext.Current.Session["user"] = currUser;

            return SetupRightsForUserRole(currUser);
        }

        private ActionResult SetupRightsForUserRole(UserModel user)
        {
            ActionResult result = null;
            switch (user.Role)
            {
                case "Director":
                    result = RedirectToAction("Index", "Director");
                    break;
                case "Secretary":
                    result = RedirectToAction("Index", "Secretary");
                    break;
                case "Teacher":
                    result = RedirectToAction("Index", "Teacher");
                    break;
                case "Student":
                    result = RedirectToAction("Index", "Student");
                    break;          
            }
            return result;
        }

        private UserModel VerifyUser(string login, string password)
        {
            UserModel signedUser;

            var adminUser = repository.Administrators.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (adminUser != null)
            {
                signedUser = new UserModel { Login = adminUser.Login, FirstName = adminUser.FirstName, LastName = adminUser.LastName, Role = adminUser.Role };
                return signedUser;
            }

            var teacherUser = repository.Teachers.FirstOrDefault(x => x.PIN == login && x.Password == password);

            if (teacherUser != null)
            {
                signedUser = new UserModel { Login = teacherUser.PIN, FirstName = teacherUser.FirstName, LastName = teacherUser.LastName, Role = "Teacher" };
                return signedUser;
            }

            var studentUser = repository.Students.FirstOrDefault(x => x.PIN == login && x.Password == password);

            if (studentUser != null)
            {
                signedUser = new UserModel { Login = studentUser.PIN, FirstName = studentUser.FirstName, LastName = studentUser.LastName, Role = "Student" };
                return signedUser;
            }

            return null;
        }

        public ActionResult LoginFailed()
        {
            return View();
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["user"] = null;

            return RedirectToAction("Login");
        }

        public string Setup()
        {
            repository.DeleteDatabase();

            repository.AddAdministrator(new Administrator { FirstName = "Director", Login = "admin", Password = "123", Role = "Director" });
            repository.AddAdministrator(new Administrator { FirstName = "Secretary1", Login = "secretary1", Password = "123", Role = "Secretary" });
            repository.AddAdministrator(new Administrator { FirstName = "Secretary2", Login = "secretary2", Password = "123", Role = "Secretary" });

            repository.AddStudent(new Student { PIN = "2010", FirstName = "Adam", LastName = "Sandler", Password = "123", Class_Id = 1, PhoneNumber = "0792893345", Address = "Chisniau, str Mircea cel Baatrin 3, ap 104"});
            repository.AddStudent(new Student { PIN = "2011", FirstName = "Jim",  LastName = "Carrey", Password = "123", Class_Id = 1, PhoneNumber = "0792893345", Address = "Chisniau, str Mircea cel Baatrin 3, ap 104" });
            repository.AddStudent(new Student { PIN = "2012", FirstName = "Anne", LastName = "Hathaway", Password = "123", Class_Id = 1, PhoneNumber = "0792893345", Address = "Chisniau, str Mircea cel Baatrin 3, ap 104" });
            repository.AddStudent(new Student { PIN = "2013", FirstName = "Robert", LastName = "John Downey", Password = "123", Class_Id = 2, PhoneNumber = "0792893345", Address = "Chisniau, str Mircea cel Baatrin 3, ap 104" });
            repository.AddStudent(new Student { PIN = "2014", FirstName = "Chris", LastName = "Evans", Password = "123", Class_Id = 3, PhoneNumber = "0792893345", Address = "Chisniau, str Mircea cel Baatrin 3, ap 104" });
            repository.AddStudent(new Student { PIN = "2015", FirstName = "Chris", LastName = "Hemsworth", Password = "123", Class_Id = 4, PhoneNumber = "0792893345", Address = "Chisniau, str Mircea cel Baatrin 3, ap 104" });


            repository.AddClass(new Class { Name = "12-A" });
            repository.AddClass(new Class { Name = "12-B" });
            repository.AddClass(new Class { Name = "11-A" });
            repository.AddClass(new Class { Name = "11-B" });

            List<Class> classes = repository.Classes.ToList();
            repository.AddTeacher(new Teacher { PIN = "2021", FirstName = "Emma", LastName = "Watson", Password = "123", Discipline_Id = 1, EducationalGrade = "USM Biology faculty", ClassesToStudy = classes });


            repository.AddDiscipline(new Discipline { Subject = "Biology" });
            repository.AddDiscipline(new Discipline { Subject = "History" });
            repository.AddDiscipline(new Discipline { Subject = "Mathematic" });
            repository.AddDiscipline(new Discipline { Subject = "Chemistry" });
            repository.AddDiscipline(new Discipline { Subject = "Physics" });

            #region Marks
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 1),
                Teacher_PIN = "2011",
                Discipline_Id = 1,
                Value = 10
            });
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 4),
                Teacher_PIN = "2011",
                Discipline_Id = 2,
                Value = -1
            });
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 11),
                Teacher_PIN = "2011",
                Discipline_Id = 2,
                Value = 9
            });
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 15),
                Teacher_PIN = "2011",
                Discipline_Id = 3,
                Value = 9
            });
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 21),
                Teacher_PIN = "2011",
                Discipline_Id = 1,
                Value = 6
            });
            repository.AddMark(new Mark
            {
                Student_PIN = "2010",
                Date = new DateTime(2016, 5, 28),
                Teacher_PIN = "2011",
                Discipline_Id = 4,
                Value = -1
            });
            #endregion

            //repository.context.SaveChanges();
            return "database was setuped";
        }

    }
}
