using System.Web.Mvc;

namespace CyberCooperativeManagementSystem.Areas.Common
{
    public class CommonAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Common";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //context.MapRoute(
            //// name: "Default",
            ////url: "{controller}/{action}/{id}",
            ////defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
            //"Controllers_default",
            //"Controllers/{controller}/{action}/{id}",
            //new { Controller = "Account", action = "Register", id = UrlParameter.Optional }
            //);
            context.MapRoute(
                "Common_default",
                "Common/{controller}/{action}/{id}",
                new {Controller="Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "CyberCooperativeManagementSystem.Areas.Common.Controllers" }


            );

            
        }
    }
}