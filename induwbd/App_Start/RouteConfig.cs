using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace induwbd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Rutas para la gestión de productos
            routes.MapRoute(
                name: "Producto",
                url: "Productos/{action}/{id}",
                defaults: new { controller = "Producto", action = "Index", id = UrlParameter.Optional }
            );

            // Rutas para la gestión de usuarios
            routes.MapRoute(
                name: "Usuario",
                url: "Usuarios/{action}/{id}",
                defaults: new { controller = "Usuario", action = "Index", id = UrlParameter.Optional }
            );

            // Rutas para el soporte técnico
            routes.MapRoute(
                name: "SoporteTecnico",
                url: "SoporteTecnico/{action}/{id}",
                defaults: new { controller = "SoporteTecnico", action = "Index", id = UrlParameter.Optional }
            );

            // Ruta de la página de inicio
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Producto", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
