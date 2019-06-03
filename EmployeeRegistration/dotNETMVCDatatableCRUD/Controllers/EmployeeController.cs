using dotNETMVCDatatableCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dotNETMVCDatatableCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (EventSchedulerEntities db = new EventSchedulerEntities())
            {
                List<EmployeeOne> employeeList = db.EmployeeOnes.ToList();
                return Json(new { data = employeeList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new EmployeeOne());
            else
            {
                using (EventSchedulerEntities db = new EventSchedulerEntities())
                {
                    return View(db.EmployeeOnes.Where(x => x.EmployeeID == id).FirstOrDefault());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(EmployeeOne employee)
        {
            using (EventSchedulerEntities db = new EventSchedulerEntities())
            {
                if (employee.EmployeeID == 0)
                {
                    db.EmployeeOnes.Add(employee);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (EventSchedulerEntities db = new EventSchedulerEntities())
            {
                EmployeeOne emp = db.EmployeeOnes.Where(x => x.EmployeeID == id).FirstOrDefault();
                db.EmployeeOnes.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}