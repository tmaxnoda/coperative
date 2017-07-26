using CyberCooperative_DAL.RepositoryBase;
using CyberCooperative_Model;
using CyberCooperativeManagementSystem.Areas.Admin.Models;
using CyberCooperativeManagementSystem.Areas.CustomFilter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    //[AuthLog(Roles = "Admin")]
    public class MemberRegistrationController : AdminBasedController
    {
        protected IRepository<Employee_Table> _employee;
        protected IRepository<ContributionPattern_history_Table> _contributionPartHistory;
       

        public MemberRegistrationController(IRepository<Employee_Table> employee,IRepository<ContributionPattern_history_Table> contributionPartHistory)
        {
            _contributionPartHistory = contributionPartHistory;
            _employee = employee;
            _context = new CoperativeDB();
        }

        [Authorize]

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult IsAlreadyAMember(string RegistrationNumber)
        {
            return Json(IsUserAvailable(RegistrationNumber));
        }

        public bool IsUserAvailable(string regnumbers)
        {
            // Takes care of remote validation
            var singleEmolyee = _context.Employee_Table.ToList();
            
            var regnumber = (from u in singleEmolyee
                             where u.RegistrationNumber.ToUpper() == regnumbers.ToUpper()
                             select new { regnumbers }).FirstOrDefault();

            bool status;
            if (regnumber != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
           
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadImages, Employee_Table employee)
        {
            Employee_Table em = new Employee_Table();
            //Intialize image
            var image = uploadImages;

            // Try catch eaceptions
            try
            {
                
             
                //Validate and  check if the current image being iterated has a fie content greater than Zero
                if (ModelState.IsValid)
                {
                    if (image == null)
                    {
                        //TempData["Message"] = "Please upload Image";
                        //this.FlashError("", "Sorry,Please upload Image");
                        Danger("Please upload Image!");
                        
                        return RedirectToAction("Index");
                    }
                    if (image != null || image.ContentLength > 0)
                    {
                        //Declare byte array for holding images to bytlike file
                        byte[] imageData = null;
                        //geth extention
                        var filename = Path.GetExtension(image.FileName);

                        // validate extension
                        if (!ValidateExtension(filename))
                        {
                            //TempData["Message"] = "upload picture with Extention .jpg|.png|.jpeg";
                            //this.FlashError("", "upload picture with Extention .jpg|.png|.jpeg");
                            Danger("upload picture with Extention .jpg|.png|.jpeg");
                            return RedirectToAction("Index");

                        }


                        // read image to binary files
                        using (var binaryReader = new BinaryReader(image.InputStream))
                        {

                            imageData = binaryReader.ReadBytes(image.ContentLength);
                            var headerImage = new HeaderImage()
                            {
                                ImageData = imageData,
                                ImageName = image.FileName,
                                IsActive = true

                            };
                            //var data = Convert.ToBase64String(imageData);
                            employee.Photo = imageData;
                          

                                //int count = _context.Employee_Table
                                //    .Where(s => s.RegistrationNumber == employee.RegistrationNumber).ToList().Count();

                                if (!_context.Employee_Table.Any(s => s.RegistrationNumber== employee.RegistrationNumber))
                            {
                              
                                em.Month = employee.Month.ToUpper();
                                em.AccountNumber = employee.AccountNumber;
                                em.ContactAddress = employee.ContactAddress;
                                em.Date = employee.Date;
                                em.Department = employee.Department.ToUpper(); 
                                em.Email = employee.Email;
                                em.FirstName = employee.FirstName.ToUpper();
                                em.LastName = employee.LastName.ToUpper();
                                em.Gender = employee.Gender;
                                em.IsActive = employee.IsActive;
                                em.MonthlySavings = employee.MonthlySavings;
                                em.NextOfKin = employee.NextOfKin;
                                em.NextOfKinRelationship = employee.NextOfKinRelationship;
                                em.NextOfKinTelephoneNumber = employee.NextOfKinTelephoneNumber;
                                em.NumberOfSharesAppliedFor = employee.NumberOfSharesAppliedFor;
                                em.Occupation = employee.Occupation;
                                em.PhoneNumber = employee.PhoneNumber;
                                em.Photo = employee.Photo;
                                em.RegistrationNumber= employee.RegistrationNumber.ToUpper();
                                em.PostalAddress = employee.PostalAddress;
                                em.ValuesOfShares = employee.ValuesOfShares;
                                em.Email = employee.Email;
                               
                                _employee.insert(em);

                            }
                            else
                            {
                                Danger("Member with Reg Number : " + employee.RegistrationNumber + " already exist");
                                return RedirectToAction("Index");
                            }
                            _employee.commit();
                        }


                        ContributionPattern_history_Table contribution = new ContributionPattern_history_Table()
                        {
                            EmployeeId = em.Id,
                            AccountNumber = em.AccountNumber,
                            RegistrationNumber = em.RegistrationNumber.ToUpper(),
                            Amount = em.MonthlySavings,
                            Month = em.Month.ToUpper(),
                            Year = em.Date.Value.ToString("yyyy"),
                            FullName = (em.FirstName + " " + em.LastName).ToUpper()


                        };

                        _contributionPartHistory.insert(contribution);
                        _contributionPartHistory.commit();
                        Success("Memeber created successfully", true);

                        return RedirectToAction("Index");
                    }


                      

                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {

                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                    TempData["Message"] = "Member form submission failed";
                }
                throw raise;
            }
            return View("Index");
        }

    }
}