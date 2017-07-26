namespace CyberCooperative_Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Loan_Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loan_Table()
        {
            LoanRepayment_Table = new HashSet<LoanRepayment_Table>();
            LoanTrasactionTimelines = new HashSet<LoanTrasactionTimeline>();
        }

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
        [Required]
        public DateTime? MonthEnding { get; set; }
        //[Required]
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

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        public int? GuarantorIdTwo { get; set; }

        public int? LoanInterestConfigId { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalMonthlyRepaymentWithContributions { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalLoanRepaid { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalLoanDue { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public virtual Employee_Table Employee_Table { get; set; }

        public virtual LoanInterestConfiguration_Table LoanInterestConfiguration_Table { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanRepayment_Table> LoanRepayment_Table { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanTrasactionTimeline> LoanTrasactionTimelines { get; set; }
    }
}
