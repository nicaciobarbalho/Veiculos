using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Veiculos.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          

             routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
           "PaginaComprar",
            url: "Comprar/",
            defaults: new { controller = "Comprar", action = "Index" }
           );

            routes.MapRoute(
        "StartBrowse",
        "Comprar/{s1}",
        new
        {   
            controller = "Comprar",
            action = "Index",            
            s1 = UrlParameter.Optional,
        });

        }
    }
}
