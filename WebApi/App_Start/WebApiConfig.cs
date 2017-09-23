using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using WebApi.Models;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // OData configuration
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Movie>("ODataMovie");

            // Activate possibility to filter and stuff globably.
            // This can be done with attributes on each method as well.
            config.Select().Expand().Filter().OrderBy().MaxTop(null).Count();

            config.MapODataServiceRoute(
                routeName: "OData Route",
                routePrefix: "odata",
                model: builder.GetEdmModel());
        }
    }
}
