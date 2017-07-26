using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CyberCooperative_Model
{
    public class EmployeeValidation
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name must not exceed 50 characters.")]

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "last name must not exceed 50 characters.")]

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Postal address must not exceed 50 characters.")]
        [Display(Name = "Postal address")]
        public string PostalAddress { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Contact must not exceed 50 characters.")]
        [Display(Name = "Home Address")]
        public string ContactAddress { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Occupation must  not exceed 50 characters.")]
        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Next of kin must not exceed 50 characters.")]
        [Display(Name = "Next of kin")]
        public string NextOfKin { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Next of kin Marital status")]
        public string NextOfKinRelationship { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Monthly savings must not exceed 50 characters")]
        [Display(Name = "Next of kin Phone Number")]
        public string NextOfKinTelephoneNumber { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "Monthly Savings")]
        public decimal MonthlySavings { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Number of shares must not exceed 50")]
        [Display(Name = "Number of Shares")]
        public string NumberOfSharesAppliedFor { get; set; }

        //[Required]
        [Column(TypeName = "money")]
        [Display(Name = "value of Shares")]
        public decimal ValuesOfShares { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Year")]
        public System.DateTime Date { get; set; }

        //[Display(Name = "Month")]
        //[Required]
        //public string Month { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }


        [Required]
  
        //[Remote("IsAlreadyAMember", "MemberRegistration", HttpMethod = "POST", ErrorMessage = "Registration Number already exists in database.")]
        [StringLength(20, ErrorMessage = "Registration number must not  exceed 20 characters")]
        [Display(Name = "Reg Number")]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(12, ErrorMessage = "Phone number must not  exceed 12 characters")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Department")]
        public string Department { get; set; }


        [Display(Name = "Account Num")]
        public string AccountNumber { get; set; }

        [Display(Name = "Image")]
        public byte[] Photo { get; set; }


    }


    [MetadataType(typeof(EmployeeValidation))]
    public partial class Employee_Table
    {

        //public Employee_Table()
        //{
        //    this.Month = this.Month.ToUpper();
        //}
        [NotMapped]
        [Display(Name = "Name")]
        public virtual string FullName
        {
            get
            {
                return FirstName + "" + LastName;
            }

        }
    }
}
