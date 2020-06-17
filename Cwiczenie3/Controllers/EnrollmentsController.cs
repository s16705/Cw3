using Cw3.DAL;
using Cw3.DTOs.Requests;
using Cw3.ModelsCw10;
using Cwiczenie3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Cw3.Controllers
{
    [ApiController]
    
    public class EnrollmentsController : ControllerBase
    {
        private readonly IStudentsDbService _dbService;

        public EnrollmentsController(IStudentsDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        [Route("api/enrollments")]
        public IActionResult EnrollStudent(EnrollStudentRequest student)
        {
            var db = new s16705Context();

            var idStudy = db.Studies.Where(s => s.Name == student.Studies).First().IdStudy;
            if (!db.Enrollment.Any(e => (e.Semester == 1) && (e.IdStudy == idStudy)))
            {
                var e = new ModelsCw10.Enrollment();
                e.IdEnrollment = db.Enrollment.Max(e => e.IdEnrollment) + 1;
                e.Semester = 1;
                e.IdStudy = idStudy;
                e.StartDate = DateTime.Now;

                db.Enrollment.Add(e);

            }

            var s = new ModelsCw10.Student();
            s.IndexNumber = student.IndexNumber;
            s.FirstName = student.FirstName;
            s.LastName = student.LastName;
            s.BirthDate = Convert.ToDateTime(student.Birthdate);
            s.IdEnrollment = db.Enrollment.Where(e => (e.Semester == 1) && (e.IdStudy == idStudy)).First().IdEnrollment;

            db.Student.Add(s);
            try
            {
                db.SaveChanges();

            }catch(Exception e)
            {
                Console.WriteLine("Error when adding new student: "+e.Source);
            }

            return Ok();

            //Old

            /*
            var response = _dbService.EnrollStudent(student);
            if(response.Info == "201 Student dodany")
            {
                return Created(response.Info, response);
            }
            else
            {
                return BadRequest(response);
            }
            */
        }

        [HttpPost]
        [Route("api/enrollments/promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest promote)
        {
            var db = new s16705Context();
            var semestr = promote.Semester;
            var idStudy = db.Studies.Where(s => s.Name == promote.Studies).First().IdStudy;
            var tmp = promote.Semester + 1;

            //var s = db.Enrollment.Where(s => (s.IdStudy == idStudy) && (s.Semester == promote.Semester));
            //db.Entry(e).Property(e => e.Semester).CurrentValue = promote.Semester+1;
            var query = "UPDATE Enrollment SET semester = "+tmp+" WHERE Semester = "+semestr+" AND IdStudy = "+idStudy;
            var pr = db.Enrollment.FromSqlRaw(query).ToList();

            db.SaveChanges();

            return Ok();
            //Old
            /*
            var response = _dbService.PromoteStudents(promote.Semester, promote.Studies);
            if (response.Info == "201 Semestr zmieniony")
            {
                return Created(response.Info, response);

            }
            else
            {
                return BadRequest(response);
            }
            */
        }

    }
}