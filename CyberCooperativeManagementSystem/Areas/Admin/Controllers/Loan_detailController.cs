using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CyberCooperative_Model;
using System.Web.Security;
using CyberCooperativeManagementSystem.Areas.CustomFilter;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    [AuthLog(Roles = "Admin")]
    public class Loan_detailController : AdminBasedController
    {
        private CoperativeDB db = new CoperativeDB();

        // GET: Admin/Loan_detail
        public async Task<ActionResult> Index()
        {
            var loan_Table = db.Loan_Table.Include(l => l.Employee_Table).Include(l => l.LoanInterestConfiguration_Table).ToListAsync();
            return View(await loan_Table);
        }

        // GET: Admin/Loan_detail/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan_Table loan_Table = await db.Loan_Table.FindAsync(id);
            if (loan_Table == null)
            {
                return HttpNotFound();
            }
            return View(loan_Table);
        }

        // GET: Admin/Loan_detail/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName");
            ViewBag.LoanInterestConfigId = new SelectList(db.LoanInterestConfiguration_Table, "id", "id");
            return View();
        }

        // POST: Admin/Loan_detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EmployeeId,AmountBorrowed,MonthBegin,YearBegin,RepaymentPeriod,RepaymentInterest,MonthlyRepaymentAmount,MonthEnding,YearEnding,MonthBeginString,YearBeginString,MonthEndingString,YearEndingString,loanStatus,GuarantorIdOne,AccountNumber,RegistrationNumber,GuarantorIdTwo,LoanInterestConfigId,TotalMonthlyRepaymentWithContributions,TotalLoanRepaid,TotalLoanDue,Name")] Loan_Table loan_Table)
        {
            if (ModelState.IsValid)
            {
                db.Loan_Table.Add(loan_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loan_Table.EmployeeId);
            ViewBag.LoanInterestConfigId = new SelectList(db.LoanInterestConfiguration_Table, "id", "id", loan_Table.LoanInterestConfigId);
            return View(loan_Table);
        }

        // GET: Admin/Loan_detail/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan_Table loan_Table = await db.Loan_Table.FindAsync(id);
            if (loan_Table == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loan_Table.EmployeeId);
            ViewBag.LoanInterestConfigId = new SelectList(db.LoanInterestConfiguration_Table, "id", "id", loan_Table.LoanInterestConfigId);
            return View(loan_Table);
        }

        // POST: Admin/Loan_detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EmployeeId,AmountBorrowed,MonthBegin,YearBegin,RepaymentPeriod,RepaymentInterest,MonthlyRepaymentAmount,MonthEnding,YearEnding,MonthBeginString,YearBeginString,MonthEndingString,YearEndingString,loanStatus,GuarantorIdOne,AccountNumber,RegistrationNumber,GuarantorIdTwo,LoanInterestConfigId,TotalMonthlyRepaymentWithContributions,TotalLoanRepaid,TotalLoanDue,Name")] Loan_Table loan_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Success("Member updateted successfully");
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loan_Table.EmployeeId);
            ViewBag.LoanInterestConfigId = new SelectList(db.LoanInterestConfiguration_Table, "id", "id", loan_Table.LoanInterestConfigId);
            return View(loan_Table);
        }

        // GET: Admin/Loan_detail/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan_Table loan_Table = await db.Loan_Table.FindAsync(id);
            if (loan_Table == null)
            {
                return HttpNotFound();
            }
            return View(loan_Table);
        }

        // POST: Admin/Loan_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Loan_Table loan_Table = await db.Loan_Table.FindAsync(id);
            db.Loan_Table.Remove(loan_Table);
            await db.SaveChangesAsync();
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
