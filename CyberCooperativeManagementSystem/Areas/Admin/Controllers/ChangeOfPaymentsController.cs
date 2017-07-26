using CyberCooperative_DAL.RepositoryBase;
using CyberCooperative_Model;
using CyberCooperativeManagementSystem.Areas.Admin.Models;
using CyberCooperativeManagementSystem.Areas.CustomFilter;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    [AuthLog(Roles = "Admin")]
    public class ChangeOfPaymentsController : AdminBasedController
    {
        protected IRepository<Employee_Table> _employee;
        protected IRepository<ContributionPattern_history_Table> _Contribution_Pattern_History;
        protected new CoperativeDB _context;
        public ChangeofMonthlyPaymentViewModel _changePaymentModel;

        public object FillPatternType { get; private set; }

        public ChangeOfPaymentsController(IRepository<Employee_Table> employee)
        {
            _employee = employee;
            _changePaymentModel = new ChangeofMonthlyPaymentViewModel();
            _context = new CoperativeDB();
        }
        // GET: Admin/ChangeOfPayments
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ChangeofMonthlyPaymentViewModel changePayments)
        {

            try
            {


                if (ModelState.IsValid)
                {

                    _changePaymentModel.InsertChangeOfPaymentForm(changePayments);
                    Success("Memeber Monthly payment Changed successfully", true);
                    return RedirectToAction("Index");
                }
                else
                {
                    Danger("Somthing seems not right!!");
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
                Danger("Somthing seems not right!!" + e.Message);
                return RedirectToAction("Index");


            }

        }


        public ActionResult  GenerateTemplate()
        {
            var employee = _context.Employee_Table.ToList();
            DataTable dtb = new DataTable();
            dtb.Columns.Add("Name", typeof(string));
            dtb.Columns.Add("RegistrationNumber", typeof(string));
            dtb.Columns.Add("Amount", typeof(decimal));
            dtb.Columns.Add("AccountNumber", typeof(string));

            for (var i=0; i < employee.Count; i++)
            {
                dtb.Rows.Add(employee[i].FirstName + " " + employee[i].LastName, employee[i].RegistrationNumber, employee[i].MonthlySavings, employee[i].AccountNumber);

            }

            HSSFWorkbook book = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)book.CreateSheet();
            HSSFCellStyle headerCellStyle = (HSSFCellStyle)book.CreateCellStyle();
            HSSFCellStyle dataCellStyle = (HSSFCellStyle)book.CreateCellStyle();

            HSSFFont font = (HSSFFont)book.CreateFont();
            font.FontHeightInPoints = 12;
            headerCellStyle.SetFont(font);

            headerCellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.SkyBlue.Index;
            headerCellStyle.FillPattern = FillPattern.SolidForeground;
            headerCellStyle.BorderTop = BorderStyle.Thin;
            headerCellStyle.BorderBottom = BorderStyle.Thin;
            headerCellStyle.BorderLeft = BorderStyle.Thin;
            headerCellStyle.BorderRight = BorderStyle.Thin;

            //dataCellStyle.BorderTop = BorderStyle.Thin;
            //dataCellStyle.BorderBottom = BorderStyle.Thin;
            //dataCellStyle.BorderLeft = BorderStyle.Thin;
            //dataCellStyle.BorderRight = BorderStyle.Thin;

            sheet.SetColumnWidth(0, 30 * 256);
            sheet.SetColumnWidth(1, 30 * 256);
            sheet.SetColumnWidth(2, 30 * 256);
            sheet.SetColumnWidth(3, 30 * 256);

            var hrow = sheet.CreateRow(0);
            for(var j=0; j < dtb.Columns.Count; j++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(0);
                row.CreateCell(j).SetCellValue(dtb.Columns[j].ColumnName.ToString());
                row.GetCell(j).CellStyle = headerCellStyle;

            }

            for (var k = 0; k < employee.Count; k++)
            {
                var drow = sheet.CreateRow(k + 1);
                HSSFRow row = (HSSFRow)sheet.GetRow(k + 1);
                for(var i =0; i < dtb.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(dtb.Rows[k][i].ToString());
                    //row.GetCell(i).CellStyle = dataCellStyle;
                }
              

            }

            MemoryStream output = new MemoryStream();
            book.Write(output);

            return File(output.ToArray(), "application/vnd.ms-excel", "template.xls");

        }

        public JsonResult searchForRegNumber(string q)
        {
            var searchData = _employee.getAll().Where(x => x.RegistrationNumber.StartsWith(q.ToLower()))
            .Select(f => new
            {
                RegNum = f.RegistrationNumber,
                Id = f.Id
            }).ToList();

            return Json(searchData, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetPreviousPayment(int id)
        {
            decimal generatePreviousPayment = getPreviousPayment(id);
            return Json(generatePreviousPayment, JsonRequestBehavior.AllowGet);

            //return View(getPreviousPayment);
        }

        public decimal getPreviousPayment(int id)
        {
            decimal mothlySavings = 0;
            var employee = _context.Employee_Table;
            if (employee.Any(x => x.Id == id))
            {
                mothlySavings = employee.Where(c => c.Id == id).Select(x => x.MonthlySavings).First().Value;

            }

            return mothlySavings;

        }

        [HttpGet]
        public ActionResult getNameResult(int id)
        {
            string generatePreviousName = getName(id);
            return Json(generatePreviousName, JsonRequestBehavior.AllowGet);

            //return View(getPreviousPayment);
        }

        public string getName(int id)
        {
            string name=string.Empty;
            var employee = _context.Employee_Table;
            if (employee.Any(x => x.Id == id))
            {
                name = employee.Where(c => c.Id == id).Select(x => x.LastName + " " + x.FirstName).First();

            }

            return name;

        }


    }
 }