﻿using System;
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
                defaults: new { controller = "DetailApi", action = "GetAllRoom", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetSpecificRoom",
                routeTemplate: "api/HotelDetail/GetSpecificRoom",
                defaults: new { controller = "DetailApi", action = "GetSpecificRoom", id = RouteParameter.Optional }
            );

           config.Routes.MapHttpRoute(
                name: "GetHotelFacilities",
                routeTemplate: "api/HotelDetail/GetHotelFacilities",
                defaults: new { controller = "DetailApi", action = "GetHotelFacilities", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "GetEachHotelOneRoom",
                routeTemplate: "api/HotelDetail/GetEachHotelOneRoom",
                defaults: new { controller = "DetailApi", action = "GetEachHotelOneRoom", id = RouteParameter.Optional }
            );

            //Irene
            config.Routes.MapHttpRoute(
               name: "GetAllSearchFicilities",
               routeTemplate: "api/Search/GetAllSearchFicilities",
               defaults: new { controller = "Search", action = "GetAllSearchFicilities", id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
                name: "GetRoomDetailMonth",
                routeTemplate: "api/Function/RoomDetailMonth",
                defaults: new { controller = "FunctionApi", action = "GetRoomDetailMonth", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "EditRoomDetail",
                routeTemplate: "api/Function/EditRoomDetail",
                defaults: new { controller = "FunctionApi", action = "EditRoomDetail", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
