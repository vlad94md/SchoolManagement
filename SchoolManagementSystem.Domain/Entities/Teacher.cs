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
        public string PersonalIdentificationNumber { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Address { get; set; }
        string PhoneNumber { get; set; }
        string MobilePhoneNumber { get; set; }
        string EducationalGrade { get; set; }
        string Email { get; set; }
        Discipline Discipline { get; set; }
        List<Class> ClassesToStudy { get; set; }
    }
}
