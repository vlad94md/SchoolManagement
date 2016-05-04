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
            /// rid of public here
            //repository.context.Database.ExecuteSqlCommand("delete from Administrators");
            //repository.context.Database.ExecuteSqlCommand("delete from Students");
            //repository.context.Database.ExecuteSqlCommand("delete from Teachers");
            //repository.context.Database.ExecuteSqlCommand("delete from Classes");
            //repository.context.Database.ExecuteSqlCommand("delete from Marks");
            //repository.context.Database.ExecuteSqlCommand("delete from Disciplines");

            repository.context.Administrators.Add(new Administrator { FirstName = "Director", Login = "admin", Password = "123", Role = "Director" });
            repository.context.Administrators.Add(new Administrator { FirstName = "Secretary1", Login = "secretary1", Password = "123", Role = "Secretary" });
            repository.context.Administrators.Add(new Administrator { FirstName = "Secretary2", Login = "secretary2", Password = "123", Role = "Secretary" });

            repository.context.Students.Add(new Student { PIN = "2010", FirstName = "Adam", LastName = "Sandler", Password = "123" });

            repository.context.Teachers.Add(new Teacher { PIN = "2011", FirstName = "Emma", LastName = "Watson", Password = "123" });

            repository.context.Classes.Add(new Class { Name = "12-A" });

            repository.context.Disciplines.Add(new Discipline { Subject = "Biology" });

            repository.context.Marks.Add(new Mark
            {
                Student_PIN = "2010",
                Date = DateTime.Now,
                Teacher_PIN = "2011",
                Discipline_Id = 1,
                Value = 10
            });

            repository.context.SaveChanges();
            return "database was setuped";
        }

    }
}
