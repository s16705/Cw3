using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.Models;
using Cwiczenie3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie3.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public IEnumerable<Enrollment> GetStudentsSemester(string id);
    }
}