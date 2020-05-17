using Cw3.DTOs.Requests;
using Cw3.DTOs.Response;

namespace Cw3.DAL
{
    public interface IStudentsDbService
    {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        PromoteStudentResponse PromoteStudents(int semester, string studies);

    }
}
