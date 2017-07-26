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
    public class ContributionPatternController : AdminBasedController
    {
        private CoperativeDB db = new CoperativeDB();

        // GET: Admin/ContributionPattern
        public async Task<ActionResult> Index()
        {
            var contributionPattern_history_Table = db.ContributionPattern_history_Table.Include(c => c.Employee_Table).ToListAsync();
            return View(await contributionPattern_history_Table);
        }

        // GET: Admin/ContributionPattern/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContributionPattern_history_Table contributionPattern_history_Table = await db.ContributionPattern_history_Table.FindAsync(id);
            if (contributionPattern_history_Table == null)
            {
                return HttpNotFound();
            }
            return View(contributionPattern_history_Table);
        }

        // GET: Admin/ContributionPattern/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName");
            return View();
        }

        // POST: Admin/ContributionPattern/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,EmployeeId,Amount,Month,Year,RegistrationNumber,AccountNumber,FullName")] ContributionPattern_history_Table contributionPattern_history_Table)
        {
            if (ModelState.IsValid)
            {
                db.ContributionPattern_history_Table.Add(contributionPattern_history_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", contributionPattern_history_Table.EmployeeId);
            return View(contributionPattern_history_Table);
        }

        // GET: Admin/ContributionPattern/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContributionPattern_history_Table contributionPattern_history_Table = await db.ContributionPattern_history_Table.FindAsync(id);
            if (contributionPattern_history_Table == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", contributionPattern_history_Table.EmployeeId);
            return View(contributionPattern_history_Table);
        }

        // POST: Admin/ContributionPattern/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,EmployeeId,Amount,Month,Year,RegistrationNumber,AccountNumber,FullName")] ContributionPattern_history_Table contributionPattern_history_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contributionPattern_history_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Success("Member updateted successfully");
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employee_Table, "Id", "FirstName", contributionPattern_history_Table.EmployeeId);
            return View(contributionPattern_history_Table);
        }

        // GET: Admin/ContributionPattern/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContributionPattern_history_Table contributionPattern_history_Table = await db.ContributionPattern_history_Table.FindAsync(id);
            if (contributionPattern_history_Table == null)
            {
                return HttpNotFound();
            }
            return View(contributionPattern_history_Table);
        }

        // POST: Admin/ContributionPattern/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ContributionPattern_history_Table contributionPattern_history_Table = await db.ContributionPattern_history_Table.FindAsync(id);
            db.ContributionPattern_history_Table.Remove(contributionPattern_history_Table);
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
