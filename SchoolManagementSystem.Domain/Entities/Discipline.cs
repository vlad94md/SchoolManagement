using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Discipline
    {
        [Key]
        public int Id { get; set; }
        string subject { get; set; }
    }
}
