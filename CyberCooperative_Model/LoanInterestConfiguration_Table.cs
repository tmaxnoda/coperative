namespace CyberCooperative_Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LoanInterestConfiguration_Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoanInterestConfiguration_Table()
        {
            Loan_Table = new HashSet<Loan_Table>();
        }

        public int? LowerBound { get; set; }

        public int? UpperBound { get; set; }

        public decimal? Interest { get; set; }

        public int id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loan_Table> Loan_Table { get; set; }
    }
}
