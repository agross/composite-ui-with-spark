using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

using Web.Modularity;

namespace Web.Application
{
  public class Package : WebPackage
  {
    public override void Register(IKernel container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines)
    {
      container
        .Register(Classes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c
                                      .Named(c.Implementation.Name.ToLowerInvariant())
                                      .LifestylePerWebRequest()));
    }
  }
}
