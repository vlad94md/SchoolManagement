using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Teacher
    {
        [Key]
        public string PIN { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string EducationalGrade { get; set; }
        public string Email { get; set; }

        public virtual Discipline Discipline { get; set; }

        public virtual List<Class> ClassesToStudy { get; set; }
    }
}
