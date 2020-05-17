using Cw3.Models;
using Cwiczenie3.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Cwiczenie3.DAL
{
    public class MockDbServiceController : IDbService
    {


        public IEnumerable<Student> GetStudents()
        {
            var _students = new List<Student>();
            using (var con = new SqlConnection("Data Source=db-mssql; Initial Catalog=s16705; Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from Student";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    _students.Add(st);
                }

                return _students;

            }

        }

        public IEnumerable<Enrollment> GetStudentsSemester(string id)
        {
            var _students = new List<Enrollment>();
            using (var con = new SqlConnection("Data Source=db-mssql; Initial Catalog=s16705; Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT Semester FROM Enrollment INNER JOIN Student ON Student.IdEnrollment=Enrollment.IdEnrollment WHERE Student.IndexNumber=@id";
                com.Parameters.AddWithValue("id", id);

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var en = new Enrollment();
//                    en.IdEnrollment = int.Parse(dr["IdEnrollment"].ToString());
                    en.Semester = Int32.Parse(dr["Semester"].ToString());
                    _students.Add(en);
                }
            }
            
            return _students;
        }

    }
}