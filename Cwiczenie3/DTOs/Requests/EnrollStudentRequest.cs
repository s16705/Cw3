using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        //[RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }

        //[MaxLength(24)]
        public string FirstName { get; set; }

        //[MaxLength(255)]
        public string LastName { get; set; }

        public string Birthdate { get; set; }

        public string Studies { get; set; }


    }
}
