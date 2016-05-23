using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Models
{
    public class TeacherViewModel
    {
        public Teacher Teacher { get; set; }
        public string Discipline { get; set; }
    }
}