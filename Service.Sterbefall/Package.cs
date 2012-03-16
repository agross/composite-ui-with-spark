using System.Collections.Generic;

using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

using Service.Sterbefall.Persistence;

using Web.Modularity;

namespace Service.Sterbefall
{
  public class Package : WebPackage
  {
    public override void Register(IKernel container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines)
    {
      RegisterDefault(container, routes, viewEngines, "Sterbefall");
      
      container.Register(Component
                           .For<ISterbefallRepository>()
                           .ImplementedBy<InMemorySterbefallRepository>()
                           .LifestyleSingleton());

    }
  }
}
