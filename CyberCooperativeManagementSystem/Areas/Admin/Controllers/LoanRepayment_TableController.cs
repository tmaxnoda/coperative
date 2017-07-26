using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CyberCooperative_Model;
using CyberCooperativeManagementSystem.Areas.CustomFilter;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    [AuthLog(Roles = "Admin")]
    public class LoanRepayment_TableController : AdminBasedController
    {
        private CoperativeDB db = new CoperativeDB();

        // GET: Admin/LoanRepayment_Table
        public ActionResult Index()
        {
            var loanRepayment_Table = db.LoanRepayment_Table.Include(l => l.Employee_Table).Include(l => l.Loan_Table);
            return View(loanRepayment_Table.ToList());
        }

        // GET: Admin/LoanRepayment_Table/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanRepayment_Table loanRepayment_Table = db.LoanRepayment_Table.Find(id);
            if (loanRepayment_Table == null)
            {
                return HttpNotFound();
            }
            return View(loanRepayment_Table);
        }

        // GET: Admin/LoanRepayment_Table/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName");
            ViewBag.LoanId = new SelectList(db.Loan_Table, "Id", "RepaymentPeriod");
            return View();
        }

        // POST: Admin/LoanRepayment_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,FullName,RegistrationNumber,Amount,Month,Year,AccountNumber,Department,RealLoanPayment,MonthlyContribution,LoanId")] LoanRepayment_Table loanRepayment_Table)
        {
            if (ModelState.IsValid)
            {
                db.LoanRepayment_Table.Add(loanRepayment_Table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loanRepayment_Table.EmployeeId);
            ViewBag.LoanId = new SelectList(db.Loan_Table, "Id", "RepaymentPeriod", loanRepayment_Table.LoanId);
            return View(loanRepayment_Table);
        }

        // GET: Admin/LoanRepayment_Table/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanRepayment_Table loanRepayment_Table = db.LoanRepayment_Table.Find(id);
            if (loanRepayment_Table == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loanRepayment_Table.EmployeeId);
            ViewBag.LoanId = new SelectList(db.Loan_Table, "Id", "RepaymentPeriod", loanRepayment_Table.LoanId);
            return View(loanRepayment_Table);
        }

        // POST: Admin/LoanRepayment_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,FullName,RegistrationNumber,Amount,Month,Year,AccountNumber,Department,RealLoanPayment,MonthlyContribution,LoanId")] LoanRepayment_Table loanRepayment_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanRepayment_Table).State = EntityState.Modified;
                db.SaveChanges();
                Success("Member updateted successfully");
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loanRepayment_Table.EmployeeId);
            ViewBag.LoanId = new SelectList(db.Loan_Table, "Id", "RepaymentPeriod", loanRepayment_Table.LoanId);
            return View(loanRepayment_Table);
        }

        // GET: Admin/LoanRepayment_Table/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanRepayment_Table loanRepayment_Table = db.LoanRepayment_Table.Find(id);
            if (loanRepayment_Table == null)
            {
                return HttpNotFound();
            }
            return View(loanRepayment_Table);
        }

        // POST: Admin/LoanRepayment_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoanRepayment_Table loanRepayment_Table = db.LoanRepayment_Table.Find(id);
            db.LoanRepayment_Table.Remove(loanRepayment_Table);
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
