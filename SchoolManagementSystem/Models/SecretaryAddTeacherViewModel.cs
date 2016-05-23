using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Models
{
    public class SecretaryAddTeacherViewModel
    {
        public List<Discipline> Disciplines { get; set; }
        public List<Class> Classes { get; set; } 
    }
}