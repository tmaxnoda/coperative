using CyberCooperativeManagementSystem.Areas.Account.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyberCooperativeManagementSystem.Areas.Account.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext context;
        public RoleController()
        {
            context = new ApplicationDbContext();
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        //public ActionResult Index()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {


        //        if (!isAdminUser())
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    var Roles = context.Roles.ToList();
        //    return View(Roles);
        //}

        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }


        private bool isAdminUser()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Create  a New role
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        /// <summary>
        /// Create a New Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}