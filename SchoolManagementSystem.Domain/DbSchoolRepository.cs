using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Domain.Entities;

namespace SchoolManagementSystem.Domain
{
    public class DbSchoolRepository
    {
        private DbSchoolContext context = new DbSchoolContext();

        public IEnumerable<Administrator> Administrators
        {
            get { return context.Administrators; }
        }

        public IEnumerable<Teacher> Teachers
        {
            get { return context.Teachers; }
        }

        public IEnumerable<Student> Students
        {
            get { return context.Students; }
        }

        public IEnumerable<Class> Classes
        {
            get { return context.Classes; }
        }

        public IEnumerable<Discipline> Disciplines
        {
            get { return context.Disciplines; }
        }

        public IEnumerable<Mark> Marks
        {
            get { return context.Marks; }
        }

        public int AddAdministrator(Administrator item)
        {
            context.Administrators.Add(item);
            return context.SaveChanges();
        }

        public int AddTeacher(Teacher item)
        {
            context.Teachers.Add(item);
            return context.SaveChanges();
        }

        public int AddStudent(Student item)
        {
            context.Students.Add(item);
            return context.SaveChanges();
        }

        public int AddClass(Class item)
        {
            context.Classes.Add(item);
            return context.SaveChanges();
        }

        public int AddDiscipline(Discipline item)
        {
            context.Disciplines.Add(item);
            return context.SaveChanges();
        }

        public int AddMark(Mark item)
        {
            context.Marks.Add(item);
            return context.SaveChanges();
        }

        //public IEnumerable<ToDoRecord> Records
        //{
        //    get { return context.Records; }
        //}

        //public int AddRecord(ToDoRecord toDo)
        //{
        //    context.Records.Add(toDo);
        //    return context.SaveChanges();
        //}

        //public int MakeDoneRecord(Guid recordId)
        //{
        //    var recordToBeDone = context.Records.FirstOrDefault(x => x.Id == recordId);
        //    if (recordToBeDone != null)
        //    {
        //        recordToBeDone.IsComplete = true;
        //    }

        //    return context.SaveChanges();
        //}

        //public int RemoveRecord(Guid recordId)
        //{
        //    var recordToDelete = context.Records.FirstOrDefault(x => x.Id == recordId);
        //    context.Records.Remove(recordToDelete);
        //    return context.SaveChanges();
        //}

        public void DeleteDatabase()
        {
            this.context.Database.Delete();
            this.context.SaveChanges();
        }
    }
}
