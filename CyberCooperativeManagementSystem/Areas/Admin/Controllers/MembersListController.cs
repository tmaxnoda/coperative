using CyberCooperative_DAL.RepositoryBase;
using CyberCooperative_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using System.Data.Entity;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    public class MembersListController : AdminBasedController
    {
       
  
        protected IRepository<Employee_Table> _employee;
        protected IRepository<ContributionPattern_history_Table> _contributionPartHistory;
        protected CoperativeDB _context;

        public MembersListController(IRepository<Employee_Table> employee, IRepository<ContributionPattern_history_Table> contributionPartHistory)
        {
            _contributionPartHistory = contributionPartHistory;
            _employee = employee;
            _context = new CoperativeDB();
        }
        // GET: Admin/MembersList
        //public ActionResult Index()
        //{
        //    var listOfEployees = _employee.getAll().ToList();
        //    return View(listOfEployees);
        //}


        //public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        //{
        //    ViewBag.CurrentSort = sortOrder;

        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "acc_desc" : "";
        //    ViewBag.RegSortParm = sortOrder == "RegistrationNumber" ? "RegNum_desc" : "RegistrationNumber";

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;


        //    var employees = from s in _context.Employee_Table
        //                        select s;
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        employees = employees.Where(s => s.RegistrationNumber.Contains(searchString)
        //                               || s.FirstName.Contains(searchString)
        //                               || s.LastName.Contains(searchString)
        //                               ||s.Date.ToString().Contains(searchString)
        //                               || s.MonthlySavings.ToString().Contains(searchString));
        //    }


        //    switch (sortOrder)
        //    {
        //        case "acc_desc":
        //            employees = employees.OrderByDescending(s => s.AccountNumber);
        //            break;
                
        //        case "RegistrationNumber":
        //            employees = employees.OrderBy(s => s.RegistrationNumber);
        //            break;
        //        case "RegNum_desc":
        //            employees = employees.OrderByDescending(s => s.RegistrationNumber);
        //            break;
        //        default:
        //            employees = employees.OrderBy(s => s.AccountNumber);
        //            break;
        //    }


        //    int pageSize = 10;
        //    int pageNumber = (page ?? 1);
        //    return View(employees.ToPagedList(pageNumber, pageSize));

        //    //return View(employees.ToList());
        //}

        public ActionResult Index()
        {

            var upper = _context.Employee_Table.OrderBy(x => x.FirstName).ToList();
          
            //var updateMonth = upper.Select(p => new Employee_Table { Month = p.Month.ToUpper() }).ToList();


            return View(upper);
        }

        ////public int AddData(string name, string address, string town, int? country){  }

        // GET: Testing/Employee_Table/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Table employee_Table = _context.Employee_Table.Find(id);
            if (employee_Table == null)
            {
                return HttpNotFound();
            }
            return View(employee_Table);
        }

        //// GET: Testing/Employee_Table/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee_Table employee_Table = _context.Employee_Table.Find(id);
        //    if (employee_Table == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee_Table);
        //}

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //     _employee.delete(id);
        //     Success("Memeber deleted successfully", true);
        //     return RedirectToAction("Index");

        //}



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Photo,FirstName,LastName,Gender,PostalAddress,ContactAddress,Occupation,NextOfKin,NextOfKinRelationship,NextOfKinTelephoneNumber,MonthlySavings,NumberOfSharesAppliedFor,ValuesOfShares,Date,IsActive,RegistrationNumber,PhoneNumber,Department,AccountNumber")] Employee_Table employee_Table)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Entry(employee_Table).State = EntityState.Modified;
        //        _context.SaveChanges();
        //        Success("Memeber Edited successfully", true);
        //        return RedirectToAction("Index");
        //    }
        //    return View(employee_Table);
        //}

        //// GET: MemberList/Employee_Table/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee_Table employee_Table = db.Employee_Table.Find(id);
        //    if (employee_Table == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee_Table);
        //}

        //// GET: MemberList/Employee_Table/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: MemberList/Employee_Table/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: MemberList/Employee_Table/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee_Table employee_Table = _context.Employee_Table.Find(id);
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
                _context.Entry(employee_Table).State = EntityState.Modified;
                _context.SaveChanges();
                Success("Memeber Edited successfully");
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
            Employee_Table employee_Table = _context.Employee_Table.Find(id);
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
            Employee_Table employee_Table = _context.Employee_Table.Find(id);
            _context.Employee_Table.Remove(employee_Table);
            _context.SaveChanges();
            Success("Memeber Deleted successfully");
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewImage(int id)
        {
            //var item = db.Employee_Table.Find(id);
            //byte[] image = item.Photo;
            //if(image==null)
            //{
            //    return File(image, "image/jpg");
            //}

            //return File(image, "image/jpg");
            var firstOrDefault = _context.Employee_Table.Where(
                                        c => c.Id == id).FirstOrDefault();


            if (firstOrDefault != null)
            {
                byte[] image = firstOrDefault.Photo;


                if (image == null)
                {
                    return RedirectToAction("Details");
                }
                else
                {
                    return File(image, "image/jpg");
                }
            }
            return View("Details");
        }
        
    }
}