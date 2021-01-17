using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyToDoAppWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Authentication routes
            config.Routes.MapHttpRoute(
                name: "Auth",
                routeTemplate: "api/auth/{id}",
                defaults: new { controller = "User", id = RouteParameter.Optional }
            );

            // Default routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
