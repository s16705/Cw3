using Cw3.DTOs.Requests;
using Cw3.DTOs.Response;
using Cwiczenie3.Models;

namespace Cw3.DAL
{
    public interface IStudentsDbService
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        PromoteStudentResponse PromoteStudents(int semester, string studies);

        //ModelsCw10.Enrollment PromoteStudent (int semester, string studies);
        Student GetStudent(string index);

    }
}
