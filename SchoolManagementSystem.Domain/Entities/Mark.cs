﻿using System;
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
        public bool Absend { get; set; }

        public virtual Discipline Discipline { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Student Student { get; set; }
    }
}