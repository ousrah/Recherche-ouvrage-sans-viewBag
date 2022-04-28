using System.Web.Mvc;

namespace WebApplication4.Areas.stagiaire
{
    public class stagiaireAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "stagiaire";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "stagiaire_default",
                "stagiaire/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}