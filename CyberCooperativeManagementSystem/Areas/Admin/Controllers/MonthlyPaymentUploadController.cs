using CyberCooperative_Model;
using CyberCooperativeManagementSystem.Areas.Admin.Models;
using CyberCooperativeManagementSystem.Areas.CustomFilter;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    [AuthLog(Roles = "Admin")]
    public class MonthlyPaymentUploadController : AdminBasedController
    {
        private CoperativeDB _db;
        decimal tottalAmonutUpload = 0;
        int count = 1;
        StringBuilder successfullyUploaded = new StringBuilder();
        StringBuilder cannotRead = new StringBuilder();
        StringBuilder builder = new StringBuilder();
        StringBuilder loanCompletedbuilder = new StringBuilder();
        StringBuilder Completedbuilder = new StringBuilder();
        StringBuilder finalbuilder = new StringBuilder();

        private MonthlyPaymentUploadViewModel motnlypy = new MonthlyPaymentUploadViewModel();

        public MonthlyPaymentUploadController()
        {
            _db = new CoperativeDB();
        }
        // GET: Admin/MonthlyPaymentUpload
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult GetSumationForAmountUploaded()
        //{
        //    decimal generateSumation = 0;
        //    generateSumation = generateSumation + tottalAmonutUpload;
        //    return Json(generateSumation, JsonRequestBehavior.AllowGet);

        //    //return View(getPreviousPayment);
        //}

        [HttpPost]
        public ActionResult ExcelUpload(HttpPostedFileBase file, MonthlyPaymentUploadViewModel pvm)
        {
            string fileLocation = string.Empty;
            if (file == null || file.ContentLength == 0)
            {

                Information("Please select a excel file", true);
                return View("Index");

            }
            else
            {
                //string fileExtension =
                // System.IO.Path.GetExtension(excelfile.FileName);
                if (file.FileName.EndsWith("xls") || file.FileName.EndsWith("xlsx"))
                {
                    fileLocation = Server.MapPath("~/Content/" + file.FileName);
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    file.SaveAs(fileLocation);
                }

                readExeclWithNpoi2007(fileLocation, pvm);
                if (builder.ToString() != "")
                {
                   
                    string comment = Convert.ToString(builder.ToString().Trim());                    
                    Danger("*********** " + comment + " ****************", true);
                    
                    //Information("*********** " + comment2 + " ****************" + '\n', true);
                    //return RedirectToAction("Index");

                }

                if (Completedbuilder.ToString() != "")
                {
                   
                    string comment2 = Convert.ToString(Completedbuilder.ToString().Trim());
                    Information("*********** " + comment2 + " ****************" + '\n', true);
 


                }
                if (loanCompletedbuilder.ToString() != "")
                {
                    string comment =Convert.ToString(loanCompletedbuilder.ToString().Trim());
                    Information("*********** " + comment + " ****************", true);
                    //Success("Transaction Uploaded successfully", true);


                }

                if (cannotRead.ToString() != "")
                {
                    string comment = Convert.ToString(cannotRead.ToString().Trim());
                    Danger("*********** " + comment + " ****************", true);
                   


                }

                if (finalbuilder.ToString() != "")
                {
                    string comment = Convert.ToString(finalbuilder.ToString().Trim());
                    Information("*********** " + comment + " ****************", true);
                    //Success("Transaction Uploaded successfully", true);
                }

               if(successfullyUploaded.ToString() != "")
                {
                    string comment = Convert.ToString(successfullyUploaded.ToString().Trim());
                    Success("************************   " + comment + "   *************************", true);
                    Information("Total amount Uploaded : " +"N " + @Convert.ToDecimal(tottalAmonutUpload).ToString("#,##0.00"),true);
                }
                return RedirectToAction("Index");

            }
        }

        private void readExeclWithNpoi2007(string fileLocation, MonthlyPaymentUploadViewModel pvm)
        {
            DataFormatter dataformat = new DataFormatter();
            XSSFWorkbook hssfworkbook;
            var lstExcel = new List<ExcelListViewModel>();

            try
            {


                using (FileStream file = new FileStream(fileLocation, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new XSSFWorkbook(file);
                }
                for (int i = 0; i < hssfworkbook.Count; i++)
                {
                    if (true)
                    {
                        ISheet sheet = hssfworkbook.GetSheetAt(i);
                        try
                        {
                            for (int j = sheet.FirstRowNum + 1; j <= sheet.LastRowNum; j++)
                            {
                                IRow row = sheet.GetRow(j);
                                if (row != null)
                                {



                                    ExcelListViewModel excel = new ExcelListViewModel();
                                    excel.Name = dataformat.FormatCellValue(row.GetCell(0,MissingCellPolicy.RETURN_NULL_AND_BLANK));
                                    excel.RegistrationNumber = dataformat.FormatCellValue(row.GetCell(1, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                                    excel.Amount = Convert.ToDecimal(dataformat.FormatCellValue(row.GetCell(2, MissingCellPolicy.RETURN_NULL_AND_BLANK)));
                                    excel.AccountNumber = dataformat.FormatCellValue(row.GetCell(3, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                                    //excel.Paymentmode =Convert.ToInt16(dataformat.FormatCellValue(row.GetCell(4, MissingCellPolicy.RETURN_NULL_AND_BLANK)));

                                    


                                    lstExcel.Add(excel);
                                    tottalAmonutUpload += excel.Amount;

                                }
                                else
                                {
                                    Danger("Your Data is not arranged wisely.");
                                    break;

                                }
                                //{"Cannot get a text value from a numeric cell"}
                            }
                           
                            
                            var loanRepaymentlist = _db.LoanRepayment_Table.ToList();
                            var employeelist = _db.Employee_Table.ToList();
                            LoanTrasactionTimeline loanTimeline = new LoanTrasactionTimeline();
                            var _loanTimeline = _db.LoanTrasactionTimelines;
                            var loanList = _db.Loan_Table;
                            using (TransactionScope transcope = new TransactionScope())
                            {
                                foreach (var item in lstExcel)
                                {


                                    foreach (var employee in employeelist)
                                    {
                                        if (employee.RegistrationNumber == item.RegistrationNumber && employee.AccountNumber == item.AccountNumber)
                                        {
                                            if (item.Amount == employee.MonthlySavings /*&& item.Paymentmode == 1*/)
                                            {
                                                _db.Transaction_Table.Add(new Transaction_Table()
                                                {
                                                    Amount = item.Amount,
                                                    FullName = item.Name,
                                                    //Email = item.Email,
                                                    //PhoneNumber = item.PhoneNumber,
                                                    AccountNumber = item.AccountNumber,
                                                    RegistrationNumber = item.RegistrationNumber,
                                                    EmployeeId = employeelist.SingleOrDefault(c => c.RegistrationNumber == item.RegistrationNumber && c.AccountNumber == item.AccountNumber).Id,
                                                    Year = pvm.YearString,
                                                    Month = pvm.MonthString.ToUpper()


                                                });

                                                count += _db.SaveChanges();
                                                break;

                                                //_db.LoanRepayment_Table.Add(new LoanRepayment_Table()
                                                //{
                                                //    Amount = item
                                                //});
                                            }
                                            else

                                            if (item.Amount > employee.MonthlySavings /*&& item.Paymentmode == 2*/)
                                            {
                                                var loanstatus = _loanTimeline.ToList().LastOrDefault((loan => loan.RegistrationNumber == item.RegistrationNumber && loan.AccountNumber == item.AccountNumber)).Status.Value;
                                                if (loanstatus == true)
                                                {
                                                    var loanalreadyPaid = _loanTimeline.ToList().LastOrDefault(loan => loan.RegistrationNumber == item.RegistrationNumber && loan.AccountNumber == item.AccountNumber).TotalLoanPaid.Value;
                                                    var totalLoanDue = _loanTimeline.ToList().LastOrDefault(loan => loan.RegistrationNumber == item.RegistrationNumber && loan.AccountNumber == item.AccountNumber).TotalLoandue.Value;
                                                    if (loanalreadyPaid < totalLoanDue)
                                                    {
                                                        var monthlyContributions = employeelist.SingleOrDefault(c => c.RegistrationNumber == item.RegistrationNumber && c.AccountNumber == item.AccountNumber).MonthlySavings;
                                                        if (monthlyContributions != null)
                                                        {
                                                            _db.LoanRepayment_Table.Add(new LoanRepayment_Table()
                                                            {
                                                                EmployeeId = employeelist.SingleOrDefault(c => c.RegistrationNumber == item.RegistrationNumber && c.AccountNumber == item.AccountNumber).Id,
                                                                FullName = item.Name.ToUpper(),
                                                                RegistrationNumber = item.RegistrationNumber.ToUpper(),
                                                                AccountNumber = item.AccountNumber,
                                                                Amount = item.Amount,
                                                                Month = pvm.MonthString.ToUpper(),
                                                                Year = pvm.YearString,
                                                                LoanId = loanList.SingleOrDefault(c => c.RegistrationNumber == item.RegistrationNumber && c.AccountNumber == item.AccountNumber).Id,
                                                                MonthlyContribution = monthlyContributions,

                                                                RealLoanPayment = item.Amount - monthlyContributions


                                                            });

                                                            count += _db.SaveChanges();
                                                            //LoanTrasactionTimeline loanTimeline = new LoanTrasactionTimeline();
                                                            Loan_Table loanTb = new Loan_Table();
                                                            var updateLoan = _context.Loan_Table;
                                                            try
                                                            {
                                                                var loanSatus = _context.LoanTrasactionTimelines.ToList().LastOrDefault(x => x.RegistrationNumber == item.RegistrationNumber && x.AccountNumber == item.AccountNumber).Status;

                                                                if (loanSatus == true)
                                                                {

                                                                    var totalLoanDu = _context.Loan_Table.SingleOrDefault((x => x.RegistrationNumber == item.RegistrationNumber && x.AccountNumber == item.AccountNumber)).TotalLoanDue;
                                                                    var totalloanpaid = _context.LoanTrasactionTimelines.ToList().LastOrDefault(x => x.RegistrationNumber == item.RegistrationNumber && x.AccountNumber == item.AccountNumber).TotalLoanPaid.Value;
                                                                    loanTimeline.EmployeeId = _context.Loan_Table.SingleOrDefault(x => x.RegistrationNumber == item.RegistrationNumber && x.AccountNumber == item.AccountNumber).EmployeeId;
                                                                    loanTimeline.LoanId = _context.Loan_Table.SingleOrDefault(x => x.RegistrationNumber == item.RegistrationNumber && x.AccountNumber == item.AccountNumber).Id;
                                                                    loanTimeline.Month = DateTime.UtcNow;
                                                                    loanTimeline.Status = _context.LoanTrasactionTimelines.ToList().LastOrDefault(x => x.RegistrationNumber == item.RegistrationNumber && x.AccountNumber == item.AccountNumber).Status.Value;
                                                                    loanTimeline.TotalLoanPaid = totalloanpaid + item.Amount;
                                                                    loanTimeline.BalanceTobePaid = totalLoanDu - loanTimeline.TotalLoanPaid;
                                                                    loanTimeline.TotalLoandue = totalLoanDu;
                                                                    loanTimeline.Name = _context.Loan_Table.SingleOrDefault(x => x.RegistrationNumber == item.RegistrationNumber && x.AccountNumber == item.AccountNumber).Name;
                                                                    loanTimeline.AccountNumber = item.AccountNumber;
                                                                    loanTimeline.RegistrationNumber = item.RegistrationNumber;
                                                                    //}
                                                                    if (loanTimeline.TotalLoanPaid == totalLoanDue || loanTimeline.TotalLoanPaid > totalLoanDue)
                                                                    {
                                                                        loanTimeline.Status = false;
                                                                        loanCompletedbuilder.AppendLine(String.Format("The member with the name - {0}, and Registration Number - {1}, already completed loan debt", item.Name, item.RegistrationNumber + '\n'));
                                                                        
                                                                    }



                                                                    try
                                                                    {
                                                                        if (loanTimeline.Id > 0)
                                                                        {
                                                                            _context.Entry(loanTimeline).State = EntityState.Modified;
                                                                            _context.SaveChanges();
                                                                        }
                                                                        else
                                                                        {
                                                                            _context.Entry(loanTimeline).State = EntityState.Added;

                                                                            _context.SaveChanges();
                                                                        }


                                                                    }
                                                                    catch (OptimisticConcurrencyException)
                                                                    {
                                                                        var ctx = ((IObjectContextAdapter)_context).ObjectContext;
                                                                        ctx.Refresh(RefreshMode.ClientWins, _context.Loan_Table);
                                                                        //_context.SaveCh;
                                                                    }




                                                                }
                                                                else
                                                                {
                                                                    //Information("The following memeber with Reg:" + item.RegistrationNumber + " has completed his loan");
                                                                    finalbuilder.AppendLine(String.Format("The member with Name  - {0}, and Registration Number - {1}, does not Match Payment Record '\n'", item.Name, item.RegistrationNumber));
                                                                    //return View("Index");
                                                                }

                                                            }
                                                            catch (DbEntityValidationException dbe)
                                                            {

                                                                string message = "An Error occured Saving: ";
                                                                foreach (var ex in dbe.EntityValidationErrors)
                                                                {
                                                                    //Aggregate Errors
                                                                    string errors = ex.ValidationErrors.Select(e => e.ErrorMessage).Aggregate((ag, e) => ag + " " + e);
                                                                    message += errors;
                                                                }
                                                            }
                                                        }



                                                    } else
                                                    {

                                                        
                                                        builder.AppendLine(String.Format("The member with account amount - {0}, and Registration Number - {1}, does not Match Payment Record '\n'", item.Amount, item.RegistrationNumber ));
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    Completedbuilder.AppendLine(String.Format("The member with the name- {0}, and Registration Number - {1}, already completed loan debt '\n'", item.Name, item.RegistrationNumber));
                                                    break;
                                                }
                                                break;

                                            }
                                            else
                                            {
                                          
                                                Danger("Check the record you entered for  " + item.Name +"   xecel sheet for correction");
                                                break;
                                               
                                            }



                                        }else
                                        
                                        if(!((from c in _context.Employee_Table where c.RegistrationNumber ==item.RegistrationNumber && c.AccountNumber == item.AccountNumber select c).Any()))
                                        {
                                            builder.AppendLine(String.Format("The member with the name  - {0}, and Registration Number - {1}, does not Match any Records in the DataStore", item.Name, item.RegistrationNumber +'\n'));
                                            break;
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                            }
                               
                                transcope.Complete();
                                successfullyUploaded.AppendLine(String.Format("Transaction Uploaded successfully"));
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            if (!(ex is FileNotFoundException || ex is ArgumentException))
                                throw;
                            Console.WriteLine(ex.Message);                       
                            Danger(ex.Message.ToString(), true);
                            RedirectToAction("Index");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cannotRead.AppendLine(String.Format("cannot read the file after row ---: " + count));
              
                
              
            }
        }
    }
}