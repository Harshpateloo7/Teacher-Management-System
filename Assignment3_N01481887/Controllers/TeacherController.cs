using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3_N01481887.Models;


namespace Assignment3_N01481887.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //GET: /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }
        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);

        }
        // GET: /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
        //POST: /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        //GET: /Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //POST: /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string TeacherSalary, string EmployeeNumber, string TeacherJoinDate)
        {   //Identify that this method is running
            //Identify the inputs provided from the form
            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherSalary);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(TeacherJoinDate);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname; 
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.TeacherSalary = TeacherSalary;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.TeacherJoinDate = TeacherJoinDate;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);
            return RedirectToAction("List");
        }
        //GET: /Teacher/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            //View/Teacher/Edit.cshtml
            return View(SelectedTeacher);
        }
        //POST: /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string TeacherSalary, string EmployeeNumber, string TeacherJoinDate)
        {
            Debug.WriteLine("Trying to update Teacher Data");
            Debug.WriteLine(id);
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherSalary);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(TeacherJoinDate);

            Teacher UpdatedTeacher = new Teacher();
            UpdatedTeacher.TeacherFname = TeacherFname;
            UpdatedTeacher.TeacherLname = TeacherLname; 
            UpdatedTeacher.TeacherSalary = TeacherSalary;
            UpdatedTeacher.EmployeeNumber = EmployeeNumber;
            UpdatedTeacher.TeacherJoinDate = TeacherJoinDate;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, UpdatedTeacher);

            return RedirectToAction("Show/"+id); //("List");
        }
    }
}