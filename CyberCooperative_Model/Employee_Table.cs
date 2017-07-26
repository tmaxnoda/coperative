namespace CyberCooperative_Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee_Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee_Table()
        {
            
            ContributionPattern_history_Table = new HashSet<ContributionPattern_history_Table>();
            LoanRepayment_Table = new HashSet<LoanRepayment_Table>();
            Loan_Table = new HashSet<Loan_Table>();
            LoanTrasactionTimelines = new HashSet<LoanTrasactionTimeline>();
            Transaction_TableOld = new HashSet<Transaction_TableOld>();
            Transaction_Table = new HashSet<Transaction_Table>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string PostalAddress { get; set; }

        [StringLength(50)]
        public string ContactAddress { get; set; }

        [StringLength(50)]
        public string Occupation { get; set; }

        [StringLength(50)]
        public string NextOfKin { get; set; }

        [StringLength(50)]
        public string NextOfKinRelationship { get; set; }

        [StringLength(50)]
        public string NextOfKinTelephoneNumber { get; set; }

        [Column(TypeName = "money")]
        public decimal? MonthlySavings { get; set; }

        [StringLength(50)]
        public string NumberOfSharesAppliedFor { get; set; }

        [Column(TypeName = "money")]
        public decimal? ValuesOfShares { get; set; }

        public DateTime? Date { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Department { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        public byte[] Photo { get; set; }

        [StringLength(50)]
       
        public string Month { get; set; }


        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContributionPattern_history_Table> ContributionPattern_history_Table { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanRepayment_Table> LoanRepayment_Table { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loan_Table> Loan_Table { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanTrasactionTimeline> LoanTrasactionTimelines { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction_TableOld> Transaction_TableOld { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction_Table> Transaction_Table { get; set; }
    }
}
