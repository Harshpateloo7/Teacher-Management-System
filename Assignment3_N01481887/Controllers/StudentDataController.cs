using Assignment3_N01481887.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment3_N01481887.Controllers
{
    public class StudentDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext BlogStudent = new SchoolDbContext();

        // This controller will access the students table of our blog database
        ///<summary>
        /// Returns a list of Students in the System
        /// </summary>
        /// <example>
        /// GET: api/StudentData/ListStudents
        /// </example>
        /// <returns>
        /// A list of Students (first name and last name)
        /// </returns>
        [HttpGet]
        public IEnumerable<Student> ListStudents()
        {
            //Create an instance of a connection
            MySqlConnection Conn = BlogStudent.AccessDatabase();
            //Open the connection between the web server and database
            Conn.Open();
            //Establish a new command (Query) for database
            MySqlCommand cmd = Conn.CreateCommand();
            //SQL Query
            cmd.CommandText = "Select * FROM Students";
            //Gather result set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Student> Students = new List<Student> { };

            //loop through each row the result set

            while (ResultSet.Read())
            {
                //Access column information by the DB column name as an index
                string StudentID = ResultSet["studentid"].ToString();
                string Studentfname = ResultSet["studentfname"].ToString();
                string Studentlname = ResultSet["studentlname"].ToString();
                string Studentnumber = ResultSet["studentnumber"].ToString();

                Student NewStudent = new Student();
                NewStudent.StudentId = StudentID;
                NewStudent.StudentFname = Studentfname;
                NewStudent.StudentLname = Studentlname;
                NewStudent.StudentNumber = Studentnumber;

                //Add the Student name to the list
                Students.Add(NewStudent);
            }
            //Close the connection between the MySQL databse and web server
            Conn.Close();
            //Returns the final list of Student names
            return Students;
        }
        ///<summary>
        /// Finds an Students in the system given an ID
        /// </summary>
        /// <param name ="id">The Student Primary Key</param>
        /// <returns>An Student Object</returns>
        /// 
        [HttpGet]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            //Create an instance of a connection
            MySqlConnection Conn = BlogStudent.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Students where studentid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                string StudentID = ResultSet["studentid"].ToString();
                string Studentfname = ResultSet["studentfname"].ToString();
                string Studentlname = ResultSet["studentlname"].ToString();
                string Studentnumber = ResultSet["studentnumber"].ToString();

                NewStudent.StudentId = StudentID;
                NewStudent.StudentFname = Studentfname;
                NewStudent.StudentLname = Studentlname;
                NewStudent.StudentNumber = Studentnumber;

            }
            return NewStudent;
        }
    }
}
