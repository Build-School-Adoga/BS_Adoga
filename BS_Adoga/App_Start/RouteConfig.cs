﻿using System;
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
                name: "Login",
                url: "MemberLogin",
                defaults: new { controller = "MemberLogin", action = "MemberLogin", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HotelDetail",
                url: "HotelDetail/{hotelName}/checkin={startDate}/checkout={endDate}/room={orderRoom}/adult={adult}/child={child}",
                defaults: new { controller = "HotelDetail", action = "HotelDetail"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "HomePage", id = UrlParameter.Optional }
            );
         
        }
    }
}
