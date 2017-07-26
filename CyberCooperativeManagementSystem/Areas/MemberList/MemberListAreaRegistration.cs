using System.Web.Mvc;

namespace CyberCooperativeManagementSystem.Areas.MemberList
{
    public class MemberListAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MemberList";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MemberList_default",
                "MemberList/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}