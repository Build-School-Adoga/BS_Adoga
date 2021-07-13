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
                name: "HotelEdit",
                url: "Hotel/Edit/{hotelid}",
                defaults: new { controller = "Function", action = "HotelEdit", hotelid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HotelDetail",
                url: "Hotel/Detail/{hotelid}",
                defaults: new { controller = "Function", action = "HotelDetails", hotelid = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "HomePage", id = UrlParameter.Optional }
            );
        }
    }
}
