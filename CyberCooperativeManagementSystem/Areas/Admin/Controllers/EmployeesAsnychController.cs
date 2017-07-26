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

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    public class EmployeesAsnychController : Controller
    {
        private CoperativeDB db = new CoperativeDB();

        // GET: Admin/EmployeesAsnych
        public async Task<ActionResult> Index()
        {
            return View(await db.Employee_Table.ToListAsync());
        }

        // GET: Admin/EmployeesAsnych/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Table employee_Table = await db.Employee_Table.FindAsync(id);
            if (employee_Table == null)
            {
                return HttpNotFound();
            }
            return View(employee_Table);
        }

        // GET: Admin/EmployeesAsnych/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/EmployeesAsnych/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Gender,PostalAddress,ContactAddress,Occupation,NextOfKin,NextOfKinRelationship,NextOfKinTelephoneNumber,MonthlySavings,NumberOfSharesAppliedFor,ValuesOfShares,Date,IsActive,RegistrationNumber,PhoneNumber,Department,AccountNumber,Photo,Month,Email")] Employee_Table employee_Table)
        {
            if (ModelState.IsValid)
            {
                db.Employee_Table.Add(employee_Table);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(employee_Table);
        }

        // GET: Admin/EmployeesAsnych/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Table employee_Table = await db.Employee_Table.FindAsync(id);
            if (employee_Table == null)
            {
                return HttpNotFound();
            }
            return View(employee_Table);
        }

        // POST: Admin/EmployeesAsnych/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Gender,PostalAddress,ContactAddress,Occupation,NextOfKin,NextOfKinRelationship,NextOfKinTelephoneNumber,MonthlySavings,NumberOfSharesAppliedFor,ValuesOfShares,Date,IsActive,RegistrationNumber,PhoneNumber,Department,AccountNumber,Photo,Month,Email")] Employee_Table employee_Table)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee_Table).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employee_Table);
        }

        // GET: Admin/EmployeesAsnych/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Table employee_Table = await db.Employee_Table.FindAsync(id);
            if (employee_Table == null)
            {
                return HttpNotFound();
            }
            return View(employee_Table);
        }

        // POST: Admin/EmployeesAsnych/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee_Table employee_Table = await db.Employee_Table.FindAsync(id);
            db.Employee_Table.Remove(employee_Table);
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
