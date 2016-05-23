using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Models
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public string ClassName { get; set; }
    }
}