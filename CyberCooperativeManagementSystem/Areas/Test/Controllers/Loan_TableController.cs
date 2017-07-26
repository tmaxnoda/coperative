using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CyberCooperative_Model;

namespace CyberCooperativeManagementSystem.Areas.Test.Controllers
{
    public class Loan_TableController : Controller
    {
        private CoperativeDB db = new CoperativeDB();

        // GET: Test/Loan_Table
        public ActionResult Index()
        {
            var loan_Table = db.Loan_Table.Include(l => l.Employee_Table).Include(l => l.LoanInterestConfiguration_Table);
            return View(loan_Table.ToList());
        }

        // GET: Test/Loan_Table/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan_Table loan_Table = db.Loan_Table.Find(id);
            if (loan_Table == null)
            {
                return HttpNotFound();
            }
            return View(loan_Table);
        }

        // GET: Test/Loan_Table/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName");
            ViewBag.LoanInterestConfigId = new SelectList(db.LoanInterestConfiguration_Table, "id", "id");
            return View();
        }

        // POST: Test/Loan_Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,AmountBorrowed,MonthBegin,YearBegin,RepaymentPeriod,RepaymentInterest,MonthlyRepaymentAmount,MonthEnding,YearEnding,MonthBeginString,YearBeginString,MonthEndingString,YearEndingString,loanStatus,GuarantorIdOne,AccountNumber,RegistrationNumber,GuarantorIdTwo,LoanInterestConfigId,TotalMonthlyRepaymentWithContributions,TotalLoanRepaid,TotalLoanDue,Name")] Loan_Table loan_Table)
        {
            if (ModelState.IsValid)
            {
                db.Loan_Table.Add(loan_Table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loan_Table.EmployeeId);
            ViewBag.LoanInterestConfigId = new SelectList(db.LoanInterestConfiguration_Table, "id", "id", loan_Table.LoanInterestConfigId);
            return View(loan_Table);
        }

        // GET: Test/Loan_Table/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan_Table loan_Table = db.Loan_Table.Find(id);
            if (loan_Table == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loan_Table.EmployeeId);
            ViewBag.LoanInterestConfigId = new SelectList(db.LoanInterestConfiguration_Table, "id", "id", loan_Table.LoanInterestConfigId);
            return View(loan_Table);
        }

        // POST: Test/Loan_Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,AmountBorrowed,MonthBegin,YearBegin,RepaymentPeriod,RepaymentInterest,MonthlyRepaymentAmount,MonthEnding,YearEnding,MonthBeginString,YearBeginString,MonthEndingString,YearEndingString,loanStatus,GuarantorIdOne,AccountNumber,RegistrationNumber,GuarantorIdTwo,LoanInterestConfigId,TotalMonthlyRepaymentWithContributions,TotalLoanRepaid,TotalLoanDue,Name")] Loan_Table loan_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan_Table).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loan_Table.EmployeeId);
            ViewBag.LoanInterestConfigId = new SelectList(db.LoanInterestConfiguration_Table, "id", "id", loan_Table.LoanInterestConfigId);
            return View(loan_Table);
        }

        // GET: Test/Loan_Table/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan_Table loan_Table = db.Loan_Table.Find(id);
            if (loan_Table == null)
            {
                return HttpNotFound();
            }
            return View(loan_Table);
        }

        // POST: Test/Loan_Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan_Table loan_Table = db.Loan_Table.Find(id);
            db.Loan_Table.Remove(loan_Table);
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
