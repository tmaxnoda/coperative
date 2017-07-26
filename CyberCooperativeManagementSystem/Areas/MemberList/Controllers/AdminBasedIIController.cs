using CyberCooperative_Model;
using CyberCooperativeManagementSystem.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CyberCooperativeManagementSystem.Areas.Admin.Controllers
{
    public class AdminBasedIIController : Controller
    {
        protected CoperativeDB _context;
        public AdminBasedIIController()
         {
             _context = new CoperativeDB();
         }
        // GET: Admin/AdminBased
        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }


        protected bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;

                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }


    }
}