using Cw3.DAL;
using Cw3.DTOs.Requests;
using Cwiczenie3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

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
            var response = _dbService.EnrollStudent(student);
            if(response.Info == "201 Student dodany")
            {
                return Created(response.Info, response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("api/enrollments/promotions")]
        public IActionResult PromoteStudent(PromoteStudentRequest promote)
        {
            var response = _dbService.PromoteStudents(promote.Semester, promote.Studies);
            if (response.Info == "201 Semestr zmieniony")
            {
                return Created(response.Info, response);

            }
            else
            {
                return BadRequest(response);
            }
        }

    }
}