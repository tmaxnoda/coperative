namespace CyberCooperative_Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoanTrasactionTimeline")]
    public partial class LoanTrasactionTimeline
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        public int? LoanId { get; set; }

        public DateTime? Month { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalLoandue { get; set; }

        [Column(TypeName = "money")]
        public decimal? TotalLoanPaid { get; set; }

        [Column(TypeName = "money")]
        public decimal? BalanceTobePaid { get; set; }

        public bool? Status { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        public virtual Employee_Table Employee_Table { get; set; }

        public virtual Loan_Table Loan_Table { get; set; }
    }
}
