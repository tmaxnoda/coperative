using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CyberCooperative_Model;

namespace CyberCooperativeManagementSystem.Areas.Admin.Models
{
    public class LoanRepaymentViewModel
    {
        

        public int? EmployeeId { get; set; }

      
        public string FullName { get; set; }

       
        public string RegistrationNumber { get; set; }

       
        public decimal? Amount { get; set; }

       
        public string Month { get; set; }

       
        public string Year { get; set; }

        
        public string AccountNumber { get; set; }

       
        public string Department { get; set; }

      
        public decimal? RealLoanPayment { get; set; }

      
        public decimal? MonthlyContribution { get; set; }

        public int? LoanId { get; set; }

        
    }
}