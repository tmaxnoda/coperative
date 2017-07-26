using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CyberCooperative_Model;

namespace CyberCooperativeManagementSystem.Areas.MemberList.Controllers
{
    public class Employee_TableController : MemberBasesController
    {
        private CoperativeDB db = new CoperativeDB();

        // GET: MemberList/Employee_Table
        public ActionResult Index()
        {
            return View(db.Employee_Table.ToList());
        }

        // GET: MemberList/Employee_Table/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Table employee_Table = db.Employee_Table.Find(id);
            if (employee_Table == null)
            {
                return HttpNotFound();
            }
            return View(employee_Table);
        }

        // GET: MemberList/Employee_Table/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberList/Employee_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Gender,PostalAddress,ContactAddress,Occupation,NextOfKin,NextOfKinRelationship,NextOfKinTelephoneNumber,MonthlySavings,NumberOfSharesAppliedFor,ValuesOfShares,Date,IsActive,RegistrationNumber,PhoneNumber,Department,AccountNumber,Photo,Month")] Employee_Table employee_Table)
        {
            if (ModelState.IsValid)
            {
                db.Employee_Table.Add(employee_Table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee_Table);
        }

        // GET: MemberList/Employee_Table/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Table employee_Table = db.Employee_Table.Find(id);
            if (employee_Table == null)
            {
                return HttpNotFound();
            }
            return View(employee_Table);
        }

        // POST: MemberList/Employee_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Gender,PostalAddress,ContactAddress,Occupation,NextOfKin,NextOfKinRelationship,NextOfKinTelephoneNumber,MonthlySavings,NumberOfSharesAppliedFor,ValuesOfShares,Date,IsActive,RegistrationNumber,PhoneNumber,Department,AccountNumber,Photo,Month")] Employee_Table employee_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee_Table).State = EntityState.Modified;
                db.SaveChanges();
                Success("Memeber Updated successfully");
                return RedirectToAction("Index");
            }
            return View(employee_Table);
        }

        // GET: MemberList/Employee_Table/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Table employee_Table = db.Employee_Table.Find(id);
            if (employee_Table == null)
            {
                return HttpNotFound();
            }
            return View(employee_Table);
        }

        // POST: MemberList/Employee_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee_Table employee_Table = db.Employee_Table.Find(id);
            db.Employee_Table.Remove(employee_Table);
            Success("Memeber Deleted successfully");
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
