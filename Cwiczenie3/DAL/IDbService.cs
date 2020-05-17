using System.Collections.Generic;
using Cw3.Models;
using Cwiczenie3.Models;

namespace Cwiczenie3.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
        public IEnumerable<Enrollment> GetStudentsSemester(string id);
    }
}