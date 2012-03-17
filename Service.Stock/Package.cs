using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Service.Stock.Services;

using Web.Modularity;

using System.Linq;

namespace Service.Stock
{
  public class Package : WebPackage
  {
    public override void Register(IWindsorContainer container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines)
    {
      RegisterDefault(container, routes, viewEngines, "Stock");

      container.Register(Components().ToArray());
    }

    static IEnumerable<IRegistration> Components()
    {
      yield return Component.For<IStockService>().ImplementedBy<StockService>();
    }
  }
}
