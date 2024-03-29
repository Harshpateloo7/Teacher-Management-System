﻿using Assignment3_N01481887.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment3_N01481887.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext BlogTeacher = new SchoolDbContext();

        //This Controller Will access the Teachers table of our blog database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of Teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = BlogTeacher.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY

            cmd.CommandText = "Select * FROM Teachers WHERE teacherfname like lower('%"+SearchKey+ "%') or teacherlname like lower('%"+SearchKey+"%') or teacherid like lower('%"+SearchKey+"%') or lower(concat(teacherfname,' ',teacherlname)) like lower('%"+SearchKey+"%')";
            
            cmd.Parameters.AddWithValue("@key","%" + SearchKey + "%");
            

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            
            //Creat an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };
            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherID = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherSalary = ResultSet["salary"].ToString();
                string TeacherJoinDate = ResultSet["hiredate"].ToString();

           
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherID;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.TeacherJoinDate = TeacherJoinDate;

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }
            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teachers names
            return Teachers;

        }
        /// <summary>
        /// Finds an Teacher in the system given an ID
        /// </summary>
        /// <param name="id">The Teacher primary key</param>
        /// <returns>An Teacher object</returns>
        [HttpGet]
        public Teacher FindTeacher(int id)
        {
           Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = BlogTeacher.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid = " +id;
            

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string TeacherSalary = ResultSet["salary"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string TeacherJoinDate = ResultSet["hiredate"].ToString();


                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.TeacherJoinDate = TeacherJoinDate;
            }
            return NewTeacher;
        }
        /// <summary>
        /// Delete Teacher data from the list
        /// </summary>
        /// <param name="id">Teacher ID</param>
        /// <return>
        /// Delete Data from the list when user click on Confirm Delete Button
        /// </return>
        /// <example>POST: /api/TeacherData/DeleteTeacher/id</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance connection
            MySqlConnection Conn = BlogTeacher.AccessDatabase();
            //Open connection
            Conn.Open();
            //Command for database
            MySqlCommand cmd = Conn.CreateCommand();
            //Query
            cmd.CommandText = "Delete FROM Teachers WHERE teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            //close the connection
            Conn.Close();
        }
        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Create an instance connection
            MySqlConnection Conn = BlogTeacher.AccessDatabase();
            //Open connection
            Conn.Open();
            //Command for database
            MySqlCommand cmd = Conn.CreateCommand();
            //Query
            cmd.CommandText = "insert into Teachers (teacherfname,teacherlname,salary,employeenumber,hiredate) value(@TeacherFname,@TeacherLname,@TeacherSalary,@EmployeeNumber,@TeacherJoinDate)";
            
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherSalary", NewTeacher.TeacherSalary);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherJoinDate", NewTeacher.TeacherJoinDate);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            //close the connection
            Conn.Close();

        }
        /// <summary>
        /// Update an Teacher data in the system
        /// </summary>
        /// <param name="TeacherId">The Teacher id in the database</param>
        /// <param name="TeacherFname">The Teacher first name in the system</param>
        /// <param name="TeacherLname">The Teacher last name in the system</param>
        /// <param name="TeacherSalary">The Teacher Salary in the system</param>
        /// <param name="EmployeeNumber">The Teacher Employee Number in the system</param>
        /// <param name="TeacherJoinDate">The Teacher join data and time</param>
        /// <example>
        /// POST: /api/TeacherData/UpdateTeacher
        /// REQUEST BODY/POST DATA
        /// {"TeacherID":3, "TeacherFname":"Harsh","TeacherLname":"Patel","TeacherSalary":"45","EmployeeNumber":"T563","TeacherJoinDate":"2020-12-5"}
        /// </example>
        [HttpPost]
        [Route("api/Teacherdata/updateTeacher/{TeacherId}")]
        public void UpdateTeacher(int TeacherId, [FromBody] Teacher UpdatedTeacher)
        {
            Debug.WriteLine("Teacher id is" + TeacherId);
            Debug.WriteLine("Teacher first name" + UpdatedTeacher.TeacherFname);
            Debug.WriteLine("Teacher last name"+UpdatedTeacher.TeacherLname);
            Debug.WriteLine("Teacher Salary" + UpdatedTeacher.TeacherSalary);
            Debug.WriteLine("Teacher Employee Numbber" + UpdatedTeacher.EmployeeNumber);
            Debug.WriteLine("Teacher Join Date" + UpdatedTeacher.TeacherJoinDate);


            //update the Teacher
            string query = "Update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname,salary=@TeacherSalary ,employeenumber=@EmployeeNumber,hiredate=@TeacherJoinDate where teacherid=@id";
            //Create an instance connection
            MySqlConnection Conn = BlogTeacher.AccessDatabase();
            //Open connection
            Conn.Open();
            //Command for database
            MySqlCommand cmd = Conn.CreateCommand();
            //Query

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@id", TeacherId);
            cmd.Parameters.AddWithValue("@TeacherFname", UpdatedTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", UpdatedTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@TeacherSalary", UpdatedTeacher.TeacherSalary);
            cmd.Parameters.AddWithValue("@EmployeeNumber", UpdatedTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherJoinDate", UpdatedTeacher.TeacherJoinDate);
            cmd.ExecuteNonQuery();
            Conn.Close();


        }

    }
}
