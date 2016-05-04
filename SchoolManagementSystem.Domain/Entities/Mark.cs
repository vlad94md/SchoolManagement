using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Domain.Entities
{
    public class Mark
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime Date { get; set; }

        public virtual int Discipline_Id { get; set; }
        //public virtual Discipline Discipline { get; set; }

        public virtual string Teacher_PIN { get; set; }
        //public virtual Teacher Teacher { get; set; }

        public virtual string Student_PIN { get; set; }
        //public virtual Student Student { get; set; }
    }
}
