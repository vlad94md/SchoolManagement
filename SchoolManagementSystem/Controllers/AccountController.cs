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
    public class AccountController : Controller
    {
        DbSchoolRepository repository = new DbSchoolRepository();

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            var currUser = VerifyUser(login, password);

            if (currUser != null)
            {
                System.Web.HttpContext.Current.Session["user"] = currUser;
            }

            return View();
        }

        private UserModel VerifyUser(string login, string password)
        {
            UserModel signedUser = null;

            var adminUser = repository.Administrators.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (adminUser != null)
            {
                signedUser = new UserModel { Login = adminUser.Login, FirstName = adminUser.FirstName, LastName = adminUser.LastName, Role = adminUser.Role };
            }

            var teacherUser = repository.Teachers.FirstOrDefault(x => x.PIN == login && x.Password == password);

            if (teacherUser != null)
            {
                signedUser = new UserModel { Login = teacherUser.PIN, FirstName = teacherUser.FirstName, LastName = teacherUser.LastName, Role = "Teacher" };
            }

            var studentUser = repository.Students.FirstOrDefault(x => x.PIN == login && x.Password == password);

            if (studentUser != null)
            {
                signedUser = new UserModel { Login = studentUser.PIN, FirstName = studentUser.FirstName, LastName = studentUser.LastName, Role = "Student" };
            }

            return signedUser;
        }

        public string Setup()
        {
            /// rid of public here
            repository.context.Administrators.Add(new Administrator { FirstName = "Director", Login = "admin", Password = "admin", Role = "Director" });
            repository.context.Administrators.Add(new Administrator { FirstName = "Secretary1", Login = "secretary1", Password = "admin", Role = "Secretary" });
            repository.context.Administrators.Add(new Administrator { FirstName = "Secretary2", Login = "secretary2", Password = "admin", Role = "Secretary" });

            repository.context.Students.Add(new Student { PIN = "2010", FirstName = "Adam", LastName = "Sandler", Password = "123" });

            repository.context.Teachers.Add(new Teacher { PIN = "2011", FirstName = "Emma", LastName = "Watson", Password = "123" });

            repository.context.Classes.Add(new Class { Name = "12-A" });

            repository.context.Disciplines.Add(new Discipline { Subject = "Biology" });

            repository.context.SaveChanges();
            return "db setup";
        }

    }
}
