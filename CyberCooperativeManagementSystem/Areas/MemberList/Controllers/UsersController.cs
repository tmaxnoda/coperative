using CyberCooperative_DAL.RepositoryBase;
using CyberCooperative_Model;
using CyberCooperativeManagementSystem.Areas.Account.Models;
using CyberCooperativeManagementSystem.Areas.Admin.Controllers;
using CyberCooperativeManagementSystem.Areas.CustomFilter;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CyberCooperativeManagementSystem.Areas.MemberList.Controllers
{
   
    [AuthLog(Roles = "User")]
    public class UsersController : AdminBasedIIController
    {
        string ErrorText = string.Empty;
        new CoperativeDB _context;
        IIdentity user = null;
        protected IRepository<Employee_Table> _employee;
        public UsersController(IRepository<Employee_Table> employee)
        {
            _employee = employee;
            _context = new CoperativeDB();
           
        }
        // GET: MemberList/Users
        public ActionResult Index()
        {
            List<Transaction_Table> _trasact=new List<Transaction_Table>();

            if (User.Identity.IsAuthenticated)
            {
                user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }

                else
                {
                    ViewBag.Name = user.Name;
                   

                    var EmployeeId = _context.Employee_Table.SingleOrDefault(c => c.Email == user.Name).Id;
                    if(EmployeeId != 0)
                    {
                        _trasact = _context.Transaction_Table.Where(x => x.EmployeeId == EmployeeId).OrderBy(x => x.Id).ToList();
                    }else
                    {
                        Danger("Cannot be found");
                    }
                   
                            
                   

                }
                return View(_trasact.ToList());
   
            }


            return View();
        }

        private bool isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var s = UserManager.GetRoles(user.GetUserId());

                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;

        }


       
        [HttpGet]
        public Decimal GetTotalHistoricalRecordPaymentTillDateResult()
        {
            string name = string.Empty;
            decimal totalHistoricalContributionsTillDate = 0;
            decimal GetTotalHistoricalRecordPayments=0;

            if (User.Identity.IsAuthenticated)
            {
                user = User.Identity;
                //ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";
                
                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }else
                {

               

                //if (!((from c in _context.Employee_Table where c.Email.Equals(user.Name) select c).Any()))
                //{

                //    return RedirectToAction("TotalSavings");

                //}
                var employeeIds = _context.Employee_Table.SingleOrDefault(x => x.Email.Equals(user.Name)).Id;

               
                Transaction_Table transTb = new Transaction_Table();
                var transTable = _context.Transaction_Table.ToList();
                foreach (var person in transTable)
                {
                    if (person.EmployeeId == employeeIds)
                    {

                        totalHistoricalContributionsTillDate = Math.Round(Convert.ToDecimal(totalHistoricalContributionsTillDate + person.Amount), 6);

                    }


                    }
                     GetTotalHistoricalRecordPayments = totalHistoricalContributionsTillDate;
                }
            }
           
            return GetTotalHistoricalRecordPayments /*Json(GetTotalHistoricalRecordPayments, JsonRequestBehavior.AllowGet)*/;
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
        public string getNameResult()
        {
            string generatePreviousName = string.Empty;
            string name = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                user = User.Identity;
                //ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                else
                {
                    //if (!((from c in _context.Employee_Table where c.Email.Equals(user.Name) select c).Any()))
                    //{
                    //    //Danger("Your Account Number does not match any records in the database");
                    //    return RedirectToAction("TotalSavings");
                    //    //return View("TotalSavings");
                    //}
                    var employeeIds = _context.Employee_Table.SingleOrDefault(x => x.Email.Equals(user.Name)).Id;

                    var employee = _context.Employee_Table;
                    if (employee.Any(x => x.Id == employeeIds))
                    {
                       name = employee.Where(c => c.Id == employeeIds).Select(x => x.LastName + " " + x.FirstName).First();

                    }

                   
                }
                generatePreviousName = name;
            }
            return generatePreviousName; /*Json(generatePreviousName, JsonRequestBehavior.AllowGet);*/
        }

        [HttpGet]
        public string getFUllName()
        {
            string generatePreviousName = string.Empty;
            string name = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                user = User.Identity;
                //ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                else
                {
                    
                    var employeeFirstName = _context.Employee_Table.SingleOrDefault(x => x.Email.Equals(user.Name)).FirstName;
                    var employeeLastName = _context.Employee_Table.SingleOrDefault(x => x.Email.Equals(user.Name)).LastName;


                    name = employeeFirstName + "  " + employeeLastName;

                    


                }
                generatePreviousName = name;
            }
            return generatePreviousName; /*Json(generatePreviousName, JsonRequestBehavior.AllowGet);*/
        }

        //private string RemoveWhiteSpace(string input)
        //{
        //    return new string(input.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        //}
        [HttpGet]
        public ActionResult TotalSavings()
        {
            return View();
        }
        [HttpGet]
       public ActionResult ActiveLoan()
        {
            //var _trasact;
            LoanTrasactionTimeline _status = new LoanTrasactionTimeline();
            if (User.Identity.IsAuthenticated)
            {
                user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }

                else
                {
                    


                    var EmployeeId = _context.Employee_Table.SingleOrDefault(c => c.Email == user.Name).Id;
                    if (EmployeeId!=0)
                    {
                        
                            _status = _context.LoanTrasactionTimelines.ToList().LastOrDefault(x => x.EmployeeId == EmployeeId && x.Status == true);
                        if (_status == null)
                        {
                            Warning("No active loan",false);
                            return RedirectToAction("Index");
                         }


                    }
                    else
                    {
                        Danger("Cannot be found");
                    }
                  



                }
                return View(_status);

            }


            return View();

        }
    }
}