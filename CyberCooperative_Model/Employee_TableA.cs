namespace CyberCooperative_Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee_TableA
    {
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
    }
}
