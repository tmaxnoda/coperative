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
    public class LoanTimelineController : AdminBasedController
    {
        private CoperativeDB db = new CoperativeDB();

        // GET: Admin/LoanTimeline
        public async Task<ActionResult> Index()
        {
            var loanTrasactionTimelines = db.LoanTrasactionTimelines.Include(l => l.Employee_Table).Include(l => l.Loan_Table);
            return View(await loanTrasactionTimelines.Where(x=>x.Status==true).ToListAsync());
        }

        public async Task<ActionResult> completedLoan()
        {
            var loanTrasactionTimelines = db.LoanTrasactionTimelines.Include(l => l.Employee_Table).Include(l => l.Loan_Table);
            return View(await loanTrasactionTimelines.Where(x => x.Status == false).ToListAsync());
        }

        // GET: Admin/LoanTimeline/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanTrasactionTimeline loanTrasactionTimeline = await db.LoanTrasactionTimelines.FindAsync(id);
            if (loanTrasactionTimeline == null)
            {
                return HttpNotFound();
            }
            return View(loanTrasactionTimeline);
        }

        // GET: Admin/LoanTimeline/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName");
            ViewBag.LoanId = new SelectList(db.Loan_Table, "Id", "RepaymentPeriod");
            return View();
        }

        // POST: Admin/LoanTimeline/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EmployeeId,LoanId,Month,Name,TotalLoandue,TotalLoanPaid,BalanceTobePaid,Status,AccountNumber,RegistrationNumber")] LoanTrasactionTimeline loanTrasactionTimeline)
        {
            if (ModelState.IsValid)
            {
                db.LoanTrasactionTimelines.Add(loanTrasactionTimeline);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loanTrasactionTimeline.EmployeeId);
            ViewBag.LoanId = new SelectList(db.Loan_Table, "Id", "RepaymentPeriod", loanTrasactionTimeline.LoanId);
            return View(loanTrasactionTimeline);
        }

        // GET: Admin/LoanTimeline/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanTrasactionTimeline loanTrasactionTimeline = await db.LoanTrasactionTimelines.FindAsync(id);
            if (loanTrasactionTimeline == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loanTrasactionTimeline.EmployeeId);
            ViewBag.LoanId = new SelectList(db.Loan_Table, "Id", "RepaymentPeriod", loanTrasactionTimeline.LoanId);
            return View(loanTrasactionTimeline);
        }

        // POST: Admin/LoanTimeline/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EmployeeId,LoanId,Month,Name,TotalLoandue,TotalLoanPaid,BalanceTobePaid,Status,AccountNumber,RegistrationNumber")] LoanTrasactionTimeline loanTrasactionTimeline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanTrasactionTimeline).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Success("Member updateted successfully");
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", loanTrasactionTimeline.EmployeeId);
            ViewBag.LoanId = new SelectList(db.Loan_Table, "Id", "RepaymentPeriod", loanTrasactionTimeline.LoanId);
            return View(loanTrasactionTimeline);
        }

        // GET: Admin/LoanTimeline/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanTrasactionTimeline loanTrasactionTimeline = await db.LoanTrasactionTimelines.FindAsync(id);
            if (loanTrasactionTimeline == null)
            {
                return HttpNotFound();
            }
            return View(loanTrasactionTimeline);
        }

        // POST: Admin/LoanTimeline/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LoanTrasactionTimeline loanTrasactionTimeline = await db.LoanTrasactionTimelines.FindAsync(id);
            db.LoanTrasactionTimelines.Remove(loanTrasactionTimeline);
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
