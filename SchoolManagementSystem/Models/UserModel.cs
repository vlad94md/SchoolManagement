using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class UserModel
    {
        public string Login { get; set; } // Login for admins and PIN for Students/Teacgers
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }  // 1.Director 2.Secretary 3.Teacher 4.Student
    }
}