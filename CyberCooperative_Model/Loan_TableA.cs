namespace CyberCooperative_Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Loan_TableA
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        [Column(TypeName = "money")]
        public decimal? AmountBorrowed { get; set; }

        public DateTime? MonthBegin { get; set; }

        public DateTime? YearBegin { get; set; }

        [StringLength(50)]
        public string RepaymentPeriod { get; set; }

        [Column(TypeName = "money")]
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
    }
}
