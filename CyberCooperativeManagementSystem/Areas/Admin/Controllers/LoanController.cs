using CyberCooperative_DAL.RepositoryBase;
using CyberCooperative_Model;
using CyberCooperativeManagementSystem.Areas.Admin.Models;
using CyberCooperativeManagementSystem.Areas.CustomFilter;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    [AuthLog(Roles = "Admin")]
    public class LoanController : AdminBasedController
    {
        protected IRepository<Employee_Table> _employee;
        protected IRepository<Loan_Table> _loan;
        protected IRepository<LoanRepayment_Table> _loanRepayment;
        protected IRepository<LoanInterestConfiguration_Table> _interestConfiguration;
        protected IRepository<ContributionPattern_history_Table> _contributionPartHistory;
        protected LoanViewModel _loanvm;
        public LoanController(IRepository<Employee_Table>employee,IRepository<Loan_Table> loan
            ,IRepository<LoanRepayment_Table> loanRepayment
            , IRepository<LoanInterestConfiguration_Table> interestConfiguration)
        {
            _context = new CoperativeDB();
            _loan = loan;
            _loanvm = new LoanViewModel();
            _loanRepayment = loanRepayment;
            _interestConfiguration = interestConfiguration;
        }
        // GET: Admin/Loan
        public ActionResult Index()
        {
            //ViewBag.EmployeeId = new SelectList(_employee.getAll().ToList(), "Id", "RegistrationNumber");
            //var list = _context.Employee_Table;
            //return View(list);
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoanViewModel loan)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _loanvm.insertLoan(loan);
                    Success("Memebr loan is created  successfully", true);
                    return RedirectToAction("Index");
                }else
                {
                    Danger("Somthing seems not right about your data!!",true);
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {

                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
                Danger("Somthing seems not right!!" + e.Message);
                return RedirectToAction("Index");
            }
           
           
        }
       #region 
        public JsonResult searchProdauto(string q)
        {
            var searchData = _employee.getAll().Where(x => x.RegistrationNumber.ToLower().StartsWith(q))
             .Select(f => new
             {
                 RegistrationNumber = f.RegistrationNumber,
                 Id = f.Id
             })
            .ToList();
            return Json(searchData, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult GetRegistrationNumber(int id)
        {
            string generateRegistrationNumber = getRegistrationNumber(id);
            return Json(generateRegistrationNumber, JsonRequestBehavior.AllowGet);

            //return View(getPreviousPayment);
        }

        public string getRegistrationNumber(int id)
        {
            string regnumber = string.Empty;
            // This is where to pass the active Parameter
            var employee = _context.Employee_Table;
            var IsLoanAvtive = _context.Loan_Table;
            if (employee.Any(x => x.Id == id))
            {
                regnumber = employee.Where(c => c.Id == id).Select(x => x.RegistrationNumber).First();

            }

            return regnumber;

        }


        [HttpGet]
        public ActionResult GetAccountNumber(int id)
        {
            string generateAccountNumber = getAccountNumber(id);
            return Json(generateAccountNumber, JsonRequestBehavior.AllowGet);

            //return View(getPreviousPayment);
        }

        public string getAccountNumber(int id)
        {
            string accountNumber = string.Empty;
            // This is where to pass the active Parameter
            var employee = _context.Employee_Table;
            var IsLoanAvtive = _context.Loan_Table;
            if (employee.Any(x => x.Id == id))
            {
                accountNumber = employee.Where(c => c.Id == id).Select(x => x.AccountNumber).First();

            }

            return accountNumber;

        }


        [HttpGet]
        public ActionResult GetPreviousPayment(int id)
        {
            decimal generatePreviousPayment = getPreviousPayment(id);
            return Json(generatePreviousPayment, JsonRequestBehavior.AllowGet);

            //return View(getPreviousPayment);
        }

        public decimal getPreviousPayment(int id)
        {
            decimal mothlySavings = 0;
            // This is where to pass the active Parameter
            var employee = _context.Employee_Table;
            var IsLoanAvtive = _context.Loan_Table; 
            if (employee.Any(x => x.Id == id))
            {
                mothlySavings = employee.Where(c => c.Id == id).Select(x => x.MonthlySavings).First().Value;

            }

            return mothlySavings;

        }

        public ActionResult getActiveLoanResult(int id)
        {
            bool generateActiveLoan = getActiveLoan(id);
            return Json(generateActiveLoan, JsonRequestBehavior.AllowGet);

            //return View(getPreviousPayment);
        }
        public bool getActiveLoan(int id)
        {
            // This is where to pass the active Parameter

            var members = _context.Loan_Table.ToList().FirstOrDefault(c => c.EmployeeId == id && c.loanStatus.Value);
            return (members ==  null) ? false : members.loanStatus.Value;

            //foreach (var person in members)
            //{
            //    if(person.EmployeeId == id)
            //    {
            //        if(person.loanStatus == true)
            //        {
            //            isActive = Convert.ToBoolean(person.loanStatus);
                        
            //        }
            //        break;
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //}

           

            //return isActive;

        }


        private decimal getClaculatedInterest(string borrowedMonthId, string moneyBorrowed)
        {
            decimal interest = 0;
            var loan = _context.LoanInterestConfiguration_Table;
            var count = loan.Count();
            int id = Convert.ToInt32(borrowedMonthId);

            foreach (var item in loan.ToArray())
            {
                if (id >= item.LowerBound && id <= item.UpperBound)
                {

                    interest = item.Interest.Value;
                    interest = ((interest / 100) * Convert.ToDecimal(moneyBorrowed));
                    break;
                }
                else
                {
                    continue;
                }
            }
            
            return interest;
        }

        [HttpGet]
        public ActionResult getClaculatedInterestResult(string borrowedMonthId, string moneyBorrowed)
        {
            decimal getClaculatedInt = getClaculatedInterest(borrowedMonthId,moneyBorrowed);
            return Json(getClaculatedInt, JsonRequestBehavior.AllowGet);

            //return View(getPreviousPayment);
        }
        //static bool CheckRange(this int num, int min, int max)
        //{
        //    return num > min && num < max;
        //}

        private decimal getCalculatedMonthlyLoanRepayment(string borrowedMonthId, string moneyBorrowed)
        {
            decimal monthlyLoanRepayment = 0;

            var loan = _context.LoanInterestConfiguration_Table;
            int id = Convert.ToInt32(borrowedMonthId);
            var interest = getClaculatedInterest(borrowedMonthId, moneyBorrowed);
         
                foreach (var month in loan.ToArray())
                {
                    if(id >= month.LowerBound && id <= month.UpperBound)
                    {
                        
                        monthlyLoanRepayment = ((Convert.ToDecimal(moneyBorrowed) + interest) / id);
                    break;
                    }else
                    {
                    continue;
                    }
                }
          
            return monthlyLoanRepayment;
        }


        

        [HttpGet]
        public ActionResult getCalculatedMonthlyLoanRepaymentResult(string borrowedMonthId, string moneyBorrowed)
        {
            decimal getCalculatedMonthlyLoanRepayments = getCalculatedMonthlyLoanRepayment(borrowedMonthId, moneyBorrowed);
            return Json(getCalculatedMonthlyLoanRepayments, JsonRequestBehavior.AllowGet);

            //return View(getPreviousPayment);
        }

        private DateTime getCalculatedStartTOFinishdate(string borrowedMonthId, string moneyBorrowed, string datepicker)
        {
            DateTime today = new DateTime();
            if (datepicker != string.Empty)
            {
                today = Convert.ToDateTime(datepicker);
            }else
            {
                today = Convert.ToDateTime("01/10/1843");
            }
            
            DateTime Months = new DateTime();

            var loan = _context.LoanInterestConfiguration_Table;
            int id = Convert.ToInt32(borrowedMonthId);
          
            foreach (var month in loan.ToArray())
            {
                if (id >= month.LowerBound && id <= month.UpperBound)
                {

                    Months = today.AddMonths(id);
                    break;
                }
                else
                {
                    continue;
                }
            }

          

            return Months;
        }

        

        [HttpGet]
        public ActionResult getCalculatedStartTOFinishdateResult(string borrowedMonthId, string moneyBorrowed, string datepicker)
        {
            DateTime getCalculatedStartTOFinishdates = getCalculatedStartTOFinishdate(borrowedMonthId, moneyBorrowed,datepicker);

            return Json(getCalculatedStartTOFinishdates.ToShortDateString(), JsonRequestBehavior.AllowGet);

            //return View(getPreviousPayment);
        }

        private decimal GetTotalHistoricalRecordPaymentTillDate(int id)
        {
            
            decimal totalHistoricalContributionsTillDate = 0;
            Transaction_Table transTb = new Transaction_Table();
              var transTable = _context.Transaction_Table.ToList();
            foreach  (var person in transTable)
            {
                if(person.EmployeeId == id)
                {
                   
                    totalHistoricalContributionsTillDate =Math.Round(Convert.ToDecimal(totalHistoricalContributionsTillDate + person.Amount),6);
                    
                }
            }

            return totalHistoricalContributionsTillDate;
        }

        [HttpGet]
        public ActionResult GetTotalHistoricalRecordPaymentTillDateResult(int id)
        {

            var GetTotalHistoricalRecordPayments = GetTotalHistoricalRecordPaymentTillDate(id);
            //decimal amountBorrowed = Convert.ToDecimal(moneyBorrowed);
            //if (amountBorrowed > (GetTotalHistoricalRecordPayments * 2))
            //{
            //    return Json(GetTotalHistoricalRecordPayments, JsonRequestBehavior.AllowGet);           
            //}
            return Json(GetTotalHistoricalRecordPayments, JsonRequestBehavior.AllowGet);

            
        }

        private decimal  getTotalMonthlyRepaymentByLoanRepaymentPlusMonthlyCobtributionsPayment(int id, string borrowedMonthId, string moneyBorrowed)
        {
            var MonthlyPayment = getPreviousPayment(id);
            var loanRepaymeny = getCalculatedMonthlyLoanRepayment(borrowedMonthId, moneyBorrowed);
            var TotalPayment = MonthlyPayment + loanRepaymeny;
            return TotalPayment;
        } 


        public ActionResult getTotalMonthlyRepaymentByLoanRepaymentPlusMonthlyCobtributionsPaymentResult(int id, string borrowedMonthId, string moneyBorrowed)
        {
            var JasonTotalPayment = getTotalMonthlyRepaymentByLoanRepaymentPlusMonthlyCobtributionsPayment(id, borrowedMonthId, moneyBorrowed);
            return Json(JasonTotalPayment, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}