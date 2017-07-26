using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CyberCooperativeManagementSystem.Areas.Admin.Models
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public int regno { get; set; }       
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string RegistratinNum { get; set; }

        [Column(TypeName = "money")]
        public decimal? Amount { get; set; }

        [StringLength(50)]
        public string Month { get; set; }

        [StringLength(50)]
        public string Year { get; set; }

        [StringLength(50)]
        public string AccountNum { get; set; }

        [StringLength(10)]
        public string Department { get; set; }
    }
}