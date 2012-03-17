using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.Windsor;
using Castle.Windsor.Installer;

using Web.Modularity;

namespace Service.Einaescherung
{
  public class PackageInfo
  {
    public const string ServiceName = "Einaescherung";
  }

  public class Package : WebPackage
  {
    public override void Register(IWindsorContainer container,
                                  ICollection<RouteBase> routes,
                                  ICollection<IViewEngine> viewEngines)
    {
      RegisterDefault(container, routes, viewEngines, PackageInfo.ServiceName);

      container.Install(FromAssembly.This());
    }
  }
}
