using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;

using Web.Modularity;

namespace Service.Login
{
  public class Package : WebPackage
  {
    public override void Register(IKernel container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines)
    {
      RegisterDefault(container, routes, viewEngines, "Login");
    }
}
}
