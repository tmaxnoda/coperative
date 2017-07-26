using CyberCooperative_Model;
using CyberCooperativeManagementSystem.Areas.Admin.Models;
using CyberCooperativeManagementSystem.Areas.CustomFilter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    [AuthLog(Roles = "Admin")]
    public class LoanDetailsController : Controller
    {
        // GET: Admin/LoanDetails
        private CoperativeDB _db = new CoperativeDB();
        LoanRepaymentViewModel _vm = new LoanRepaymentViewModel();
        List<LoanRepaymentViewModel> ilist = new List<LoanRepaymentViewModel>();
        public ActionResult Index()
        {
            var loanDetails = _db.Loan_Table.Include(x => x.Employee_Table).Include(x => x.LoanInterestConfiguration_Table).ToList();
            return View(loanDetails);
        }
    }
}