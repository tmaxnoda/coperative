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
using CyberCooperativeManagementSystem.Areas.CustomFilter;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    [AuthLog(Roles = "Admin")]
    public class TransactionHistoryController : AdminBasedController
    {
        private CoperativeDB db = new CoperativeDB();

        // GET: Admin/TransactionHistory
        public async Task<ActionResult> Index()
        {
            var transaction_Table = db.Transaction_Table.Include(t => t.Employee_Table).ToListAsync();
            return View(await transaction_Table);
        }

        [HttpGet]
        public async Task<ActionResult> GetTotalAmount()
        {
          
            return View();
        }


        [HttpGet]
        public ActionResult GetTotalHistoricalRecordPaymentTillDateResult(string id)
        {

            string name = string.Empty;
            var ids = Convert.ToString(id);
            if (string.IsNullOrWhiteSpace(ids))
            {

                ids = RemoveWhiteSpace(ids);
            }
            if (!((from c in _context.Employee_Table where c.RegistrationNumber.Equals(ids) select c).Any()))
            {

                return RedirectToAction("TotalSavings");

            }
            var employeeIds = _context.Employee_Table.SingleOrDefault(x => x.RegistrationNumber.Equals(ids)).Id;

            decimal totalHistoricalContributionsTillDate = 0;
            Transaction_Table transTb = new Transaction_Table();
            var transTable = _context.Transaction_Table.ToList();
            foreach (var person in transTable)
            {
                if (person.EmployeeId == employeeIds)
                {

                    totalHistoricalContributionsTillDate = Math.Round(Convert.ToDecimal(totalHistoricalContributionsTillDate + person.Amount),2);

                }


            }

            var GetTotalHistoricalRecordPayments = totalHistoricalContributionsTillDate;
            return Json(GetTotalHistoricalRecordPayments, JsonRequestBehavior.AllowGet);
            //return totalHistoricalContributionsTillDate;
        }



        //[HttpGet]
        //public ActionResult getNameResult(string id)
        //{
        //    string generatePreviousName = getName(id);
        //    return Json(generatePreviousName, JsonRequestBehavior.AllowGet);

        //    //return View(getPreviousPayment);
        //}
        [HttpGet]
        public ActionResult getNameResult(string id)
        {
            string name = string.Empty;
            var ids = Convert.ToString(id);
            if (string.IsNullOrWhiteSpace(ids))
            {

                ids = RemoveWhiteSpace(ids);
            }
            if (!((from c in _context.Employee_Table where c.RegistrationNumber.Equals(ids) select c).Any()))
            {
                //Danger("Your Account Number does not match any records in the database");
                return RedirectToAction("TotalSavings");
                //return View("TotalSavings");
            }
            var employeeIds = _context.Employee_Table.SingleOrDefault(x => x.RegistrationNumber.Equals(ids)).Id;

            var employee = _context.Employee_Table;
            if (employee.Any(x => x.Id == employeeIds))
            {
                name = employee.Where(c => c.Id == employeeIds).Select(x => x.LastName + " " + x.FirstName).First();

            }

            string generatePreviousName = name;

            return Json(generatePreviousName, JsonRequestBehavior.AllowGet);

        }

        private string RemoveWhiteSpace(string input)
        {
            return new string(input.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        }
        // GET: Admin/TransactionHistory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction_Table transaction_Table = await db.Transaction_Table.FindAsync(id);
            if (transaction_Table == null)
            {
                return HttpNotFound();
            }
            return View(transaction_Table);
        }

        // GET: Admin/TransactionHistory/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName");
            return View();
        }

        // POST: Admin/TransactionHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EmployeeId,FullName,RegistrationNumber,Amount,Month,Year,AccountNumber,Department")] Transaction_Table transaction_Table)
        {
            if (ModelState.IsValid)
            {
                db.Transaction_Table.Add(transaction_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", transaction_Table.EmployeeId);
            return View(transaction_Table);
        }

        // GET: Admin/TransactionHistory/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction_Table transaction_Table = await db.Transaction_Table.FindAsync(id);
            if (transaction_Table == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", transaction_Table.EmployeeId);
            return View(transaction_Table);
        }

        // POST: Admin/TransactionHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EmployeeId,FullName,RegistrationNumber,Amount,Month,Year,AccountNumber,Department")] Transaction_Table transaction_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Success("Member updated successfully");
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", transaction_Table.EmployeeId);
            return View(transaction_Table);
        }

        // GET: Admin/TransactionHistory/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction_Table transaction_Table = await db.Transaction_Table.FindAsync(id);
            if (transaction_Table == null)
            {
                return HttpNotFound();
            }
            return View(transaction_Table);
        }

        // POST: Admin/TransactionHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Transaction_Table transaction_Table = await db.Transaction_Table.FindAsync(id);
            db.Transaction_Table.Remove(transaction_Table);
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
