using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.Windsor;
using Castle.Windsor.Installer;

using Infrastructure;

using NServiceBus;

namespace Web.Application
{
  public class Global : HttpApplication
  {
    protected void Application_Start()
    {
      var container = new WindsorContainer();
      container.Install(FromAssembly.InThisApplication());

      Configure.WithWeb()
        .CastleWindsorBuilder(container)
        .XmlSerializer()
        .MsmqTransport()
        .IsTransactional(true)
        .PurgeOnStartup(true)
        .MsmqSubscriptionStorage()
        .UnicastBus()
        .ImpersonateSender(false)
        .CreateBus()
        .Start();

      ControllerBuilder.Current.SetControllerFactory(container.Resolve<IControllerFactory>());

      RegisterGlobalFilters(GlobalFilters.Filters, container);
      RegisterRoutes(RouteTable.Routes);
    }

    static void RegisterGlobalFilters(GlobalFilterCollection filters, IWindsorContainer container)
    {
      filters.Add(new HandleErrorAttribute());
      filters.Add(new RavenActionFilterAttribute(container));
    }

    static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
    }
  }
}
