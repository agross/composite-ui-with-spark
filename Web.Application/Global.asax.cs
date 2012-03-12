using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Web.Application
{
  public class Global : HttpApplication
  {
    protected void Application_Start()
    {
      RegisterGlobalFilters(GlobalFilters.Filters);
      RegisterRoutes(RouteTable.Routes);

      var container = new WindsorContainer();
      container.Install(FromAssembly.InThisApplication());

      ControllerBuilder.Current.SetControllerFactory(container.Resolve<IControllerFactory>());
    }

    static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }

    static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
                      "Default",
                      // Route name
                      "{controller}/{action}/{id}",
                      // URL with parameters
                      new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
        );
    }
  }
}
