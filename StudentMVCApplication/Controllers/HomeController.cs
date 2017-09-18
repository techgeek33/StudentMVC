using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentMVCApplication.Models;

namespace StudentMVCApplication.Controllers
{
    public class HomeController : Controller
    {
        private StudentDBEntities dbContext = new StudentDBEntities();
        public ActionResult Index()
        {
            var model = (from student in dbContext.Students
                         select student).ToList();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            Student student = dbContext.Students.First(x => x.StudentID == id);
            if(student == null)
            {
                return RedirectToAction("Index");
            }
            return View("Details", student);
        }

        public ActionResult Delete(int id)
        {
            Student student = dbContext.Students.First(x => x.StudentID == id);
            return View(student);   
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Student student = dbContext.Students.First(x => x.StudentID == id);
                dbContext.Students.Remove(student);
                    dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Delete Failure, see inner exception");
                return View();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Student student = new Student();
                if(ModelState.IsValid)
                {
                    student.FirstName = collection["FirstName"].ToString();
                    student.MiddleName = collection["MiddleName"].ToString();
                    student.LastName = collection["LastName"].ToString();
                    student.Email = collection["Email"].ToString();
                    student.Phone = collection["Phone"].ToString();

                    dbContext.Students.Add(student);
                    dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(student);
                }
                
            }
            catch
            {
                return View();
            }
        }

       

        public ActionResult Edit(int id)
        {
            Student student = dbContext.Students.First(x => x.StudentID == id);
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Student student = dbContext.Students.First(x => x.StudentID == id);
                if(TryUpdateModel(student))
                {
                    dbContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Edit Failure, see inner exception");
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}