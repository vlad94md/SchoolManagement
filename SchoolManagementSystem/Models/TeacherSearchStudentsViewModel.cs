using SchoolManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolManagementSystem.Models
{
    public class TeacherSearchStudentsViewModel
    {
        public List<Student> StudentsToStudy { get; set; }
        public Dictionary<string, int> ClassNames { get; set; }
    }
}