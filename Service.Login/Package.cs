using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Service.Login.Services;

using Web.Modularity;

namespace Service.Login
{
  public class Package : WebPackage
  {
    public override void Register(IWindsorContainer container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines)
    {
      RegisterDefault(container, routes, viewEngines, "Login");
      
      container.Register(Component
                           .For<IAuthenticationService>()
                           .ImplementedBy<AuthenticationService>()
                           .LifestylePerWebRequest());
    }
  }
}
