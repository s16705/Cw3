using Cw3.Controllers;
using Cw3.DTOs.Requests;
using Cw3.DTOs.Response;
using Cw3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.IIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Cw3.DAL
{
    public class SqlServerDbService : IStudentsDbService
    {
        private string sqlConnection = "Data Source=db-mssql; Initial Catalog=s16705; Integrated Security=True";
        
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response = new EnrollStudentResponse();
            response.FirstName = request.FirstName;
            response.LastName = request.LastName;
            response.IndexNumber = request.IndexNumber;
            response.Studies = request.Studies;
            response.Birthdate = request.Birthdate.Replace('.', '/');

            DateTime time = Convert.ToDateTime(response.Birthdate);
            string dbTime = time.ToString("yyyy-MM-dd HH:mm:ss.fff");

            using (var con=new SqlConnection(sqlConnection))
            using(var com=new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                
                //Czy studia istnieja
                com.CommandText = "select IdStudy from Studies where Name=@Name";
                com.Parameters.AddWithValue("name", request.Studies);

                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    tran.Rollback();
                    response.Info = "400 Wpisz prawidlowa nazwe studiow";
                    dr.Close();

                }
                int idstudies = (int)dr["IdStudy"];
                dr.Close();

                //Pobranie MAX IdEnrollment
                com.CommandText = "SELECT IdEnrollment FROM Enrollment WHERE IdEnrollment >= "
                                        + "(SELECT MAX(IdEnrollment) FROM Enrollment)";
                var dr2 = com.ExecuteReader();
                if (!dr2.Read()) { }
                int newId = (int)dr2["IdEnrollment"];
                newId++;
                dr2.Close();

                //Nowy wpis do Enrollment
                var dr3 = com.ExecuteReader();
                if (dr3.Read())
                {
                    com.CommandText = "INSERT INTO Enrollment(IdEnrollment, Semester, IdStudy, StartDate) VALUES(@IdEnrollment,@Semester,@IdStudy,@StartDate)";
                    com.Parameters.AddWithValue("IdEnrollment", newId);
                    com.Parameters.AddWithValue("Semester", 1);
                    com.Parameters.AddWithValue("IdStudy", idstudies);
                    com.Parameters.AddWithValue("StartDate", DateTime.Now);
                    dr3.Close();
                    
                    com.ExecuteNonQuery();
                    

                    response.Info = "Enrollment dodany";

                }
                dr3.Close();


                //Nowy student
                try
                {

                    com.CommandText = "INSERT INTO Student(IndexNumber, FirstName, LastName, BirthDate, idEnrollment) VALUES(@IndexNumber, @FirstName, @LastName, @BirthDate, @IdEnroll)";
                    com.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                    com.Parameters.AddWithValue("FirstName", request.FirstName);
                    com.Parameters.AddWithValue("LastName", request.LastName);
                    com.Parameters.AddWithValue("IdEnroll", newId);
                    com.Parameters.AddWithValue("BirthDate", dbTime);

                    com.ExecuteNonQuery();
                    tran.Commit();
                    response.Info = "201 Student dodany";
                }catch(SqlException e)
                {
                    response.Info = "400 Blad podczad dodawania studenta" + e;
                    tran.Rollback();
                }
   
                //Zakończenie
                con.Close();

            }
        
            return response;
        }

        public PromoteStudentResponse PromoteStudents(int semester, string studies)
        {
            PromoteStudentResponse promote = new PromoteStudentResponse();
            promote.Studies = studies;
            promote.Semester = semester;
            using (var con = new SqlConnection(sqlConnection))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;

                //Czy wpisy sa poprawne
                com.CommandText = "select * from Enrollment INNER JOIN Studies ON Enrollment.IdStudy=Studies.IdStudy where Enrollment.Semester=@semester OR Studies.Name=@name";
                com.Parameters.AddWithValue("semester", semester);
                com.Parameters.AddWithValue("name", studies);


                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    promote.Info = "400 Wpisz prawidlowa nazwe studiow";
                    dr.Close();
                    tran.Rollback();
                }
                int idStudy = (int)dr["IdStudy"];
                int nrSemester = (int)dr["semester"];
                int newSemester = nrSemester + 1;
                dr.Close();

                //Jezeli wpisy poprawne, aktualizuj semestr studentow
                try
                {
                    com.CommandText = "update Enrollment set Semester=@newSemester where IdStudy=@idStudy and Semester=@nrSemester";
                    com.Parameters.AddWithValue("newSemester", newSemester);
                    com.Parameters.AddWithValue("nrSemester", nrSemester);
                    com.Parameters.AddWithValue("idStudy", idStudy);
                    com.ExecuteNonQuery();
                    tran.Commit();
                    promote.Info = "201 Semestr zmieniony";

                }
                catch (SqlException e)
                {
                    promote.Info = "400 Blad podczad aktualizacji semestru" + e;
                    tran.Rollback();
              
                }

            }
            
            return promote;
        }
    }
}
