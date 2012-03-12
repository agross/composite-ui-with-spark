using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

using Spark.Bindings;
using Spark.FileSystem;
using Spark.Web.Mvc;

using Web.Modularity.Bindings;
using Web.Modularity.Blocks;

namespace Web.Modularity
{
  public interface IWebPackage
  {
    void Register(IKernel container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines);
  }

  public abstract class WebPackage : IWebPackage
  {
    public abstract void Register(IKernel container,
                                  ICollection<RouteBase> routes,
                                  ICollection<IViewEngine> viewEngines);

    protected void RegisterDefault(IKernel container,
                                   ICollection<RouteBase> routes,
                                   IEnumerable<IViewEngine> viewEngines,
                                   string service)
    {
      var assembly = GetType().Assembly;

      RegisterComponents(container, assembly, service);
      RegisterRoutes(routes, assembly, service);
      RegisterViewFolders(viewEngines, assembly, service);
    }

    static void RegisterComponents(IKernel container, Assembly assembly, string service)
    {
      container
        .Register(Classes
                    .FromAssembly(assembly)
                    .BasedOn<IController>()
                    .Configure(c => c.Named(c.Implementation.ScopedTo(service))
                                      .LifestylePerWebRequest()))
        .Register(Classes
                    .FromAssembly(assembly)
                    .BasedOn<IBlock>()
                    .Configure(c => c.Named(c.Implementation.ScopedTo(service))
                                      .LifestyleTransient()));

      if (service != null)
      {
        container
          .Register(Component
                      .For<IBindingProvider>()
                      .ImplementedBy<ServiceBindingProvider>()
                      .LifestyleSingleton()
                      .Named("ServiceBindingProvider".ScopedTo(service))
                      .DependsOn(Dependency.OnValue(typeof(string), service)));
      }
    }

    static void RegisterRoutes(ICollection<RouteBase> routes, Assembly assembly, string service)
    {
      if (service == null)
      {
        routes.Add(new Route("{controller}/{action}",
                             new RouteValueDictionary(new { controller = "home", action = "index" }),
                             new MvcRouteHandler()));
        return;
      }

      routes.Add(new Route("{service}/{controller}/{action}",
                           new RouteValueDictionary(new { controller = "home", action = "index" }),
                           new RouteValueDictionary(new { service }),
                           new MvcRouteHandler()));

      routes.Add(new Route("content/{service}/{*resource}",
                           new RouteValueDictionary(),
                           new RouteValueDictionary(new { service }),
                           new EmbeddedContentRouteHandler(assembly, assembly.GetName().Name + ".Content")));
    }

    static void RegisterViewFolders(IEnumerable<IViewEngine> viewEngines, Assembly assembly, string service)
    {
      if (service == null)
      {
        return;
      }

      var viewFolder = new EmbeddedViewFolder(assembly, assembly.GetName().Name + ".Views");
      var sparkViewFactory = viewEngines.OfType<SparkViewFactory>().First();

      sparkViewFactory.ViewFolder = sparkViewFactory.ViewFolder
        .Append(new SubViewFolder(viewFolder, service))
        .Append(new SubViewFolder(viewFolder, "Shared\\" + service));
    }
  }
}
