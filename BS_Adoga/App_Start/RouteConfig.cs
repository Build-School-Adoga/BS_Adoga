using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BS_Adoga
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
            name: "HotelCity",
            url: "Taiwan/{city}",
            defaults: new { controller = "Search", action = "Search", city = UrlParameter.Optional }
        );
            routes.MapRoute(
         name: "Hotel",
         url: "Taiwan/Hotel/{id}",
         defaults: new { controller = "HotelDetail", action = "Detail", id = UrlParameter.Optional }
     );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "HomePage", id = UrlParameter.Optional }
            );
         
        }
    }
}
