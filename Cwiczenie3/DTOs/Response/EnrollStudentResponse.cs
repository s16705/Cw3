using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DTOs.Response
{
    public class EnrollStudentResponse
    {
        public string Info { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IndexNumber { get; set; }
        public string Studies { get; set; }
        public string Birthdate { get; set; }

    }
}
