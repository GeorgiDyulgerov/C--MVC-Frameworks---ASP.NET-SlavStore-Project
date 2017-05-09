using System.Web.Mvc;

namespace SlavStore.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { area = "admin",action = "Index", id = UrlParameter.Optional },
                new[] { "SlavStore.Areas.Admin.Controllers"}
            );
        }
    }
}