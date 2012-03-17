using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.Windsor;

using Web.Modularity;

namespace Web.Application
{
  public class Package : WebPackage
  {
    public override void Register(IWindsorContainer container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines)
    {
      RegisterDefault(container, routes, viewEngines, null);
    }
  }
}
