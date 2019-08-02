using mvcevaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcevaluation.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            StudentDBHandle dbhandle = new StudentDBHandle();
            ModelState.Clear();
            return View(dbhandle.GetStudent());
            
        }

        public ActionResult InsertStudent()
        {

            return View();
        }
        
        // POST: Student/Create
        [HttpPost]
        public ActionResult InsertStudent(Student smodel)
        {
            try
            {
               
                    StudentDBHandle sdb = new StudentDBHandle();
                    sdb.InsertStudents(smodel);
                    
                    return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            StudentDBHandle sdb = new StudentDBHandle();
            return View(sdb.GetStudent().Find(smodel => smodel.Id == id));
        }

       
        [HttpPost]
        public ActionResult Edit(int id, Student smodel)
        {
            try
            {
                StudentDBHandle sdb = new StudentDBHandle();
                sdb.UpdateStudent(smodel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                StudentDBHandle sdb = new StudentDBHandle();
                if (sdb.DeleteStudent(id))
                {
                    ViewBag.AlertMsg = "Student Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}