namespace CyberCooperative_Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LoanRepayment_TableCOpy
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [StringLength(50)]
        public string Month { get; set; }

        [StringLength(50)]
        public string Year { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(10)]
        public string Department { get; set; }

        [Column(TypeName = "money")]
        public decimal? RealLoanPayment { get; set; }

        [Column(TypeName = "money")]
        public decimal? MonthlyContribution { get; set; }

        public int? LoanId { get; set; }
    }
}
