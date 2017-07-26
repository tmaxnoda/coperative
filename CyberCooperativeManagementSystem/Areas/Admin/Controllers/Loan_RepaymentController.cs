using CyberCooperative_Model;
using CyberCooperativeManagementSystem.Areas.Admin.Models;
using CyberCooperativeManagementSystem.Areas.CustomFilter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    [AuthLog(Roles = "Admin")]
    public class Loan_RepaymentController : AdminBasedController
    {
        private CoperativeDB _db = new CoperativeDB();
        LoanRepaymentViewModel _vm = new LoanRepaymentViewModel();
        List<LoanRepaymentViewModel> ilist = new List<LoanRepaymentViewModel>();
        // GET: Admin/Loan_Repayment
        public ActionResult Index()
        {
            var loanRepatment = _db.LoanRepayment_Table.Include(x => x.Employee_Table).Include(x => x.Loan_Table).ToList();
                                 
            return View(loanRepatment);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           var loanRepayment_Table = _db.LoanRepayment_Table.Find(id);
            if (loanRepayment_Table == null)
            {
                return HttpNotFound();
            }
            return View(loanRepayment_Table);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanRepayment_Table loanRepayment_Table = _db.LoanRepayment_Table.Find(id);
            if (loanRepayment_Table == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(_db.Employee_Table, "Id", "FirstName", loanRepayment_Table.EmployeeId);
            ViewBag.LoanId = new SelectList(_db.Loan_Table, "Id", "RepaymentPeriod", loanRepayment_Table.LoanId);
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
                _db.Entry(loanRepayment_Table).State = EntityState.Modified;
                _db.SaveChanges();
                Success("Member updateted successfully");
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_db.Employee_Table, "Id", "FirstName", loanRepayment_Table.EmployeeId);
            ViewBag.LoanId = new SelectList(_db.Loan_Table, "Id", "RepaymentPeriod", loanRepayment_Table.LoanId);
            return View(loanRepayment_Table);
        }
    }
}