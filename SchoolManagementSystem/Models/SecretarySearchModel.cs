using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class SecretarySearchModel
    {
        public List<TeacherViewModel> Teachers { get; set; }
        public List<StudentViewModel> Students { get; set; } 
    }
}