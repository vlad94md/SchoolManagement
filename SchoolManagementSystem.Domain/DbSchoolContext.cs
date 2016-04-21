using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Domain
{
    public class DbSchoolContext : DbContext
    {
        public DbSchoolContext()
        {
            //Database.SetInitializer<DbSchoolContext>(new DropCreateDatabaseIfModelChanges<DbSchoolContext>());
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
    }
}
