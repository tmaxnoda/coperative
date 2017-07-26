using CyberCooperative_DAL.RepositoryBase;
using CyberCooperative_Model;
using CyberCooperativeManagementSystem.Areas.Admin.Models;
using CyberCooperativeManagementSystem.Areas.CustomFilter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    [AuthLog(Roles = "Admin")]
    public class SigleMonthlyContributionUploadController : AdminBasedController
    {
        protected IRepository<Transaction_Table> _transactiontb;
        protected IRepository<LoanRepayment_Table> _loanRepaymenttb;
        protected IRepository<Loan_Table> _loantb;
        protected Loan_Table _loan;
        protected CoperativeDB _context;
        public Transaction_Table _Transaction;
        public LoanTrasactionTimeline _timeline;
        public LoanRepayment_Table _loanRepayment;
        private CoperativeDB db = new CoperativeDB();
        protected StringBuilder loanCompletedbuilder = new StringBuilder();
        protected StringBuilder nullexception = new StringBuilder();
        public SigleMonthlyContributionUploadController(IRepository<Transaction_Table> transactiontb, IRepository<LoanRepayment_Table> loanRepaymenttb,
            IRepository<Loan_Table> loantb)
        {
            _timeline = new LoanTrasactionTimeline();
            _Transaction = new Transaction_Table();
            _transactiontb = transactiontb;
            _loantb = loantb;
            _loanRepaymenttb = loanRepaymenttb;
            _context = new CoperativeDB();
            _loan = new Loan_Table();
            _loanRepayment = new LoanRepayment_Table();

        }
        // GET: Admin/SigleMonthlyContributionUpload
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UploadSingleMonthlyPayment(TransactionViewModel singleTrans)
        {
            //if (ModelState.IsValid)
            //{
            using (TransactionScope transcope = new TransactionScope())
            {
                if (singleTrans == null)
                {
                    Danger("Registration details  cannot be empty");
                    return View("Index");
                }

                if (singleTrans != null)
                {
                    var monthlyPayment = _context.Employee_Table.SingleOrDefault(x => x.Id == singleTrans.regno).MonthlySavings;
                    //var isActiveLoan = _context.Loan_Table.SingleOrDefault(x => x.EmployeeId == singleTrans.regno && x.loanStatus == true).loanStatus;
                    if (monthlyPayment == singleTrans.Amount)
                    {
                        _Transaction.EmployeeId = singleTrans.regno;
                        _Transaction.RegistrationNumber = singleTrans.RegistratinNum;
                        _Transaction.Amount = singleTrans.Amount;
                        _Transaction.FullName = singleTrans.Name;
                        _Transaction.Month = singleTrans.Month;
                        _Transaction.Year = singleTrans.Year;
                        _Transaction.AccountNumber = singleTrans.AccountNum;
                        _transactiontb.insert(_Transaction);

                    }
                    else if (singleTrans.Amount > monthlyPayment)
                    {
                        var loanstatus = _context.LoanTrasactionTimelines.ToList().LastOrDefault((loan => loan.RegistrationNumber == singleTrans.RegistratinNum && loan.AccountNumber == singleTrans.AccountNum)).Status;
                        if (loanstatus == true)
                        {
                            var loanalreadyPaid = _context.LoanTrasactionTimelines.ToList().LastOrDefault(loan => loan.RegistrationNumber == singleTrans.RegistratinNum && loan.AccountNumber == singleTrans.AccountNum).TotalLoanPaid;
                            var totalLoanDue = _context.LoanTrasactionTimelines.ToList().LastOrDefault(loan => loan.RegistrationNumber == singleTrans.RegistratinNum && loan.AccountNumber == singleTrans.AccountNum).TotalLoandue;
                            if (loanalreadyPaid < totalLoanDue)
                            {
                                var monthlyContributions = _context.Employee_Table.SingleOrDefault(c => c.RegistrationNumber == singleTrans.RegistratinNum && c.AccountNumber == singleTrans.AccountNum).MonthlySavings;
                                if (monthlyContributions != null)
                                {
                                    _context.LoanRepayment_Table.Add(new LoanRepayment_Table()
                                    {
                                        EmployeeId = _context.Employee_Table.SingleOrDefault(c => c.RegistrationNumber == singleTrans.RegistratinNum && c.AccountNumber == singleTrans.AccountNum).Id,
                                        FullName = singleTrans.Name,
                                        RegistrationNumber = singleTrans.RegistratinNum,
                                        AccountNumber = singleTrans.AccountNum,
                                        Amount = singleTrans.Amount,
                                        Month = singleTrans.Month,
                                        Year = singleTrans.Year,
                                        LoanId = _context.Loan_Table.SingleOrDefault(c => c.RegistrationNumber == singleTrans.RegistratinNum && c.AccountNumber == singleTrans.AccountNum).Id,
                                        MonthlyContribution = monthlyContributions,

                                        RealLoanPayment = singleTrans.Amount - monthlyContributions
                                    });
                                    _context.SaveChanges();

                                    try
                                    {
                                        var checkLoanStatus = _context.LoanTrasactionTimelines.ToList().LastOrDefault(x => x.RegistrationNumber == singleTrans.RegistratinNum && x.AccountNumber == singleTrans.AccountNum).Status;
                                        if (checkLoanStatus == true)
                                        {
                                            var totalLoanDu = _context.Loan_Table.SingleOrDefault((x => x.RegistrationNumber == singleTrans.RegistratinNum && x.AccountNumber == singleTrans.AccountNum)).TotalLoanDue;
                                            var totalloanpaid = _context.LoanTrasactionTimelines.ToList().LastOrDefault(x => x.RegistrationNumber == singleTrans.RegistratinNum && x.AccountNumber == singleTrans.AccountNum).TotalLoanPaid.Value;
                                            _timeline.EmployeeId = _context.Loan_Table.SingleOrDefault(x => x.RegistrationNumber == singleTrans.RegistratinNum && x.AccountNumber == singleTrans.AccountNum).EmployeeId;
                                            _timeline.LoanId = _context.Loan_Table.SingleOrDefault(x => x.RegistrationNumber == singleTrans.RegistratinNum && x.AccountNumber == singleTrans.AccountNum).Id;
                                            _timeline.Month = DateTime.UtcNow;
                                            _timeline.Status = _context.LoanTrasactionTimelines.ToList().LastOrDefault(x => x.RegistrationNumber == singleTrans.RegistratinNum && x.AccountNumber == singleTrans.AccountNum).Status.Value;
                                            _timeline.TotalLoanPaid = totalloanpaid + singleTrans.Amount;
                                            _timeline.BalanceTobePaid = totalLoanDu - _timeline.TotalLoanPaid;
                                            _timeline.TotalLoandue = totalLoanDu;
                                            _timeline.Name = _context.Loan_Table.SingleOrDefault(x => x.RegistrationNumber == singleTrans.RegistratinNum && x.AccountNumber == singleTrans.AccountNum).Name;
                                            _timeline.AccountNumber = singleTrans.AccountNum;
                                            _timeline.RegistrationNumber = singleTrans.RegistratinNum;

                                            if (_timeline.TotalLoanPaid == totalLoanDue || _timeline.TotalLoanPaid > totalLoanDue)
                                            {
                                                _timeline.Status = false;
                                                loanCompletedbuilder.AppendLine(String.Format("The member with the name - {0}, and Registration Number - {1}, already completed loan debt", singleTrans.Name, singleTrans.RegistratinNum + '\n'));

                                            }

                                            if (_timeline.Id > 0)
                                            {
                                                _context.Entry(_timeline).State = EntityState.Modified;
                                                _context.SaveChanges();
                                            }
                                            else
                                            {
                                                _context.Entry(_timeline).State = EntityState.Added;

                                                _context.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            Information("The following memeber with Reg:" + singleTrans.RegistratinNum + " has completed his loan");
                                            return View("Index");
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        throw ex;
                                    }
                                }
                                else
                                {
                                    Information("The following memeber with Reg:" + singleTrans.RegistratinNum + " has no monthly contribution assigned");
                                    return View("Index");
                                }
                            }
                           



                        }
                        else
                        {
                            Information("The following memeber with Reg:" + singleTrans.RegistratinNum + "has completed his loan");
                            return View("Index");
                        }



                    }
                    else
                    {
                        Danger(singleTrans.regno + "  Memeber with the Amount provided " + singleTrans.Amount + " Does not match any records");
                        return View("Index");
                    }


                }
                else
                {
                    Danger("Record cannot be empty");
                    return View("Index");
                };

                transcope.Complete();
                
            }
            Success("The Member with the name " + singleTrans.Name + " savings have been saved successfully");
            return View("Index");
        }
    }
}