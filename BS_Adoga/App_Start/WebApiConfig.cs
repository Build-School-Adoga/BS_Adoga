using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BS_Adoga
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "GetAllRoom",
                routeTemplate: "api/HotelDetail/GetAllRoom",
                defaults: new { controller="DetailApi",action="GetAllRoom",id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetSpecificRoom",
                routeTemplate: "api/HotelDetail/GetSpecificRoom",
                defaults: new { controller = "DetailApi", action = "GetSpecificRoom", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SetCheckOutData",
                routeTemplate: "api/HotelDetail/SetCheckOutData",
                defaults: new { controller = "HotelDetail", action = "SetCheckOutData", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
