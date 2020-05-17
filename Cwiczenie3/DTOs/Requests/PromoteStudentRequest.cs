using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DTOs.Requests
{
    public class PromoteStudentRequest
    {
        [Required(ErrorMessage = "400 Musisz podać kierunek studiów")]
        public string Studies { get; set; }

        [Required(ErrorMessage = "400 Musisz podać semestr")]
        public int Semester { get; set; }
    }
}
