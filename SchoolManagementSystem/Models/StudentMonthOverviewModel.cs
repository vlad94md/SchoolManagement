using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Models
{
    public class StudentMonthOverviewModel
    {
        public List<Discipline> Subjects { get; set; }
        public List<Mark> StudentMarks { get; set; }
        public int Year { get; set; }
        public int Month { get; set;}
    }
}