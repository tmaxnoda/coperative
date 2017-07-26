using CyberCooperative_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Transactions;
using System.Web;

namespace CyberCooperativeManagementSystem.Areas.Admin.Models
{
    
    public class ChangeofMonthlyPaymentViewModel
    {
        //public ChangeOfPaymentForm_Table changeOfPayment { get; set; }
        protected CoperativeDB _datacontext;
        public ChangeofMonthlyPaymentViewModel()
        {
            _datacontext = new CoperativeDB();
        }

        public int regno { get; set; }

        [Required]
        [Display(Name = "New Monthly Savings")]
        public Decimal Amount { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        [Display(Name = "Month")]
        public string Month { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        [Display(Name = "Year")]
        public string Year { get; set; }
        //public ContributionPattern_history_Table UpdateContributionPatternHistory { get; set; }

        internal void InsertChangeOfPaymentForm(ChangeofMonthlyPaymentViewModel changePayment)
        
             
        {
            using (TransactionScope Trans = new TransactionScope())
            {
                try
                {
                    Employee_Table employee = new Employee_Table();
                    //employee = changePayment.ChangeOfFormEmployee;
                    var employeeTable = _datacontext.Employee_Table;


                    try
                    {
                        if (employeeTable.Any(s => s.Id == changePayment.regno))
                        {
                            var _employee = _datacontext.Employee_Table.SingleOrDefault(c => c.Id == changePayment.regno);

                            _employee.MonthlySavings = changePayment.Amount;
                            _employee.Month = changePayment.Month.ToUpper();
                            _employee.Date = DateTime.Now;

                            // _datacontext.Employee_Table.Attach(employee);


                            _datacontext.Entry(_employee).State = EntityState.Modified;
                            _datacontext.SaveChanges();
                            //_datacontext.Employee_Table.Add(employee);



                            //Trans.Complete();
                        };

                    }
                    catch (DbEntityValidationException dbe)
                    {

                        string message = "An Error occured Saving: ";
                        foreach (var ex in dbe.EntityValidationErrors)
                        {
                            //Aggregate Errors
                            string errors = ex.ValidationErrors.Select(e => e.ErrorMessage).Aggregate((ag, e) => ag + " " + e);
                            message += errors;
                        }
                    }





                    ContributionPattern_history_Table contributionHistory = new ContributionPattern_history_Table();
                    contributionHistory.Amount = changePayment.Amount;
                    contributionHistory.EmployeeId = changePayment.regno;
                    var __employee = _datacontext.Employee_Table.FirstOrDefault(c => c.Id == changePayment.regno);
                    contributionHistory.Month = changePayment.Month;
                    contributionHistory.Year = changePayment.Year;
                    contributionHistory.RegistrationNumber = __employee.RegistrationNumber;
                    contributionHistory.FullName = __employee.FirstName + "" + __employee.LastName;
                    _datacontext.ContributionPattern_history_Table.Add(contributionHistory);

                    _datacontext.SaveChanges();


                }
                catch (DbEntityValidationException dbe)
                {
                    //throw ex.Message;


                    //ex.Message;

                    string message = "An Error occured Saving: ";
                    foreach (var ex in dbe.EntityValidationErrors)
                    {
                        //Aggregate Errors
                        string errors = ex.ValidationErrors.Select(e => e.ErrorMessage).Aggregate((ag, e) => ag + " " + e);
                        message += errors;
                    }
                }

                Trans.Complete();

            }

        }
        
    }
}