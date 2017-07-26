using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CyberCooperative_Model;
using System.Transactions;
using System.Data.Entity.Validation;
using System.Web.Http.ModelBinding;
using CyberCooperativeManagementSystem.Areas.Admin.Controllers;

namespace CyberCooperativeManagementSystem.Areas.Admin.Models
{
    public class LoanViewModel
    {
        protected CoperativeDB _datacontext;
        protected AdminBasedController _adminBase;
        public LoanViewModel()
        {
            _adminBase = new AdminBasedController();
            _datacontext = new CoperativeDB();
        }
        public int Id { get; set; }
        [Required]
        public string RegistratinNum { get; set; }
        [Required]
        public string AccountNum { get; set; }
        public int? EmployeeId { get; set; }
        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "Loan")]
        public decimal? AmountBorrowed { get; set; }
        [Required]
        public DateTime? MonthBegin { get; set; }
   
        public DateTime? YearBegin { get; set; }

        [StringLength(50)]
        [Required]
        public string RepaymentPeriod { get; set; }

        [Column(TypeName = "money")]
        [Required]
        public decimal? RepaymentInterest { get; set; }

        [Column(TypeName = "money")]
        public decimal? MonthlyRepaymentAmount { get; set; }

        public DateTime? MonthEnding { get; set; }

        public DateTime? YearEnding { get; set; }

        [StringLength(50)]
        public string MonthBeginString { get; set; }

        [StringLength(50)]
        public string YearBeginString { get; set; }

        [StringLength(50)]
        public string MonthEndingString { get; set; }

        [StringLength(50)]
        public string YearEndingString { get; set; }

        public bool? loanStatus { get; set; }

        public int? GuarantorIdOne { get; set; }

        public int? GuarantorIdTwo { get; set; }

        public int? LoanInterestConfigId { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalMonthlyRepaymentWithContributions { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalLoanDue { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

       





        internal void insertLoan(LoanViewModel loan)
        {
            try
            {
                using (TransactionScope tsc = new TransactionScope())
                {
                    try
                    {
                        var emoployee = new Employee_Table();

                        //Fuction to check lowerbound nd upper bound
                        var data = _datacontext.LoanInterestConfiguration_Table.ToArray();
                        Func<int, int> func = (configId) =>
                        {
                            int id = 0;
                            Array.ForEach(data, c =>
                            {
                                if (configId >= c.LowerBound && configId <= c.UpperBound)
                                {
                                    id = c.id;
                                }
                            });
                            return id;
                        };

                        var employeList = _datacontext.Employee_Table;
                        if (employeList.Any(x => x.Id == loan.EmployeeId))
                        {
                            //var _loan = _datacontext.Loan_Table.SingleOrDefault(x => x.Id == loan.EmployeeId);
                            var _loan = new Loan_Table();
                            _loan.EmployeeId = loan.EmployeeId;
                            _loan.RegistrationNumber = loan.RegistratinNum;
                            _loan.AccountNumber = loan.AccountNum;
                            _loan.Name = loan.Name;
                            _loan.AmountBorrowed = loan.AmountBorrowed;
                            _loan.MonthBegin = loan.MonthBegin;
                            _loan.MonthEnding = loan.MonthEnding;
                            _loan.RepaymentInterest = loan.RepaymentInterest;
                            _loan.MonthlyRepaymentAmount = loan.MonthlyRepaymentAmount;
                            _loan.TotalMonthlyRepaymentWithContributions = loan.TotalMonthlyRepaymentWithContributions;
                            _loan.loanStatus = false;
                            _loan.GuarantorIdOne = loan.GuarantorIdOne;
                            _loan.GuarantorIdTwo = loan.GuarantorIdTwo;
                            _loan.RepaymentPeriod = loan.RepaymentPeriod;
                            _loan.LoanInterestConfigId = func.Invoke(int.Parse(loan.RepaymentPeriod));
                            _loan.MonthBeginString = Convert.ToString(loan.MonthBegin.Value.ToString("MM"));
                            _loan.MonthEndingString = Convert.ToString(loan.MonthEnding.Value.ToString("MM"));
                            //loan.YearBegin = DateTime.UtcNow;
                            //_loan.YearEnding= Convert.ToDateTime(loan.MonthEnding.Value.Year);
                            _loan.YearBeginString = Convert.ToString(loan.MonthBegin.Value.ToString("yyyy"));
                            _loan.YearEndingString = Convert.ToString(loan.MonthEnding.Value.ToString("yyyy"));
                            _loan.TotalLoanDue = loan.AmountBorrowed + loan.RepaymentInterest;
                            _loan.TotalLoanRepaid = 0;
                            _datacontext.Loan_Table.Add(_loan);
                            _datacontext.SaveChanges();

                            if (employeList.Any(x => x.Id == loan.EmployeeId))
                            {
                                var _loanTimeLine = new LoanTrasactionTimeline();
                                _loanTimeLine.EmployeeId = loan.EmployeeId;
                                var loanId = _datacontext.Loan_Table.SingleOrDefault(x => x.EmployeeId == loan.EmployeeId).Id;
                                _loanTimeLine.LoanId = loanId;
                                _loanTimeLine.Month = DateTime.UtcNow;
                                _loanTimeLine.Name = loan.Name;
                                _loanTimeLine.TotalLoandue = loan.AmountBorrowed + loan.RepaymentInterest;
                                _loanTimeLine.TotalLoanPaid = 0;
                                _loanTimeLine.RegistrationNumber = loan.RegistratinNum;
                                _loanTimeLine.AccountNumber = loan.AccountNum;
                                _loanTimeLine.Status = loan.loanStatus;
                                _loanTimeLine.BalanceTobePaid = _loanTimeLine.TotalLoandue - _loanTimeLine.TotalLoanPaid;
                                _datacontext.LoanTrasactionTimelines.Add(_loanTimeLine);
                                _datacontext.SaveChanges();

                            }
                            //var _cofigurationId = _datacontext.LoanInterestConfiguration_Table.Select(x => x.id).Where(x => x.)


                        }
                    }
                    catch (DbEntityValidationException dbe)
                    {

                        string message = "An Error occured Saving: ";
                        foreach (var ex in dbe.EntityValidationErrors)
                        {
                            //Aggregate Errors
                            string errors = ex.ValidationErrors.Select(e => e.ErrorMessage).Aggregate((ag, e) => ag + " " + e);
                            message += errors;
                            _adminBase.Danger(Convert.ToString(message), true);
                            break;
                        }
                    }
                    tsc.Complete();
                }
            }
            catch (DbEntityValidationException dbe)
            {

                string message = "An Error occured Saving: ";
                foreach (var ex in dbe.EntityValidationErrors)
                {
                    //Aggregate Errors
                    string errors = ex.ValidationErrors.Select(e => e.ErrorMessage).Aggregate((ag, e) => ag + " " + e);
                    message += errors;
                    _adminBase.Danger(Convert.ToString(message), true);
                    break;
                }
            }
           
   
        }
    }
}