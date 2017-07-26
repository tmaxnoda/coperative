using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CyberCooperativeManagementSystem.Areas.Admin.Models
{
    public class ExcelListViewModel
    {
       
        public string RegistrationNumber { get; set; }

        public int Paymentmode { get; set; }
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public System.DateTime Date { get; set; }
        ////public string PhoneNumber { get; set; }
    }
}