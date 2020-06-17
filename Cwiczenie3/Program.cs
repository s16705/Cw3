using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.ModelsCw10;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cwiczenie3
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Database first
            GetStudents();
            //updatestudent();
            //RemoveStudent();
            //InsertStudent();

            CreateHostBuilder(args).Build().Run();
        }
        
        public static void GetStudents()
        {
            var db = new s16705Context();

            var res = db.Student.ToList();

        }

        public static void UpdateStudent()
        {
            var db = new s16705Context();

            var s1 = db.Student.First();
            s1.FirstName = "Ewa";

            db.SaveChanges();

        }

        public static void RemoveStudent()
        {
            var db = new s16705Context();

            //Opcja nr1
            var s = db.Student.Where(s => s.IdEnrollment == 8).First();
            db.Student.Remove(s);

            //Opcja nr2
            //var s = new Student
            //{
            //    IdEnrollment = 8
            //};
            //db.Attach(s);
            //db.Remove(s);

            db.SaveChanges();
        }

        public static void InsertStudent()
        {
            var db = new s16705Context();

            var e = new Enrollment();
            e.IdEnrollment = db.Enrollment.Max(e => e.IdEnrollment) + 1;
            e.Semester = 1;
            e.IdStudy = 3;
            e.StartDate = DateTime.Now;

            db.Enrollment.Add(e);

            Random index = new Random();
            String indexnr = "s" + (index.Next(1000,9999));
            var s = new Student
            {
                IndexNumber = indexnr,
                FirstName = "Anna",
                LastName = "Zielińska",
                BirthDate = DateTime.Now.AddYears(-20),
                IdEnrollment = e.IdEnrollment
                
            };
            db.Student.Add(s);

            db.SaveChanges();
        }
            public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
