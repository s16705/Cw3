using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        [RegularExpression("^s[0-9]+$")]
        [Required(ErrorMessage = "400 Musisz podać numer indeksu")]
        public string IndexNumber { get; set; }

        [Required(ErrorMessage = "400 Musisz podać imię")]
        [MaxLength(24)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "400 Musisz podać nazwisko")]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "400 Musisz podać datę urodzenia")]
        public string Birthdate { get; set; }

        [Required(ErrorMessage = "400 Musisz podać typ studiów")]
        public string Studies { get; set; }


    }
}
