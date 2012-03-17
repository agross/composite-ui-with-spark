using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Raven.Client;

using Web.Modularity;

namespace Infrastructure
{
  public class Package : WebPackage
  {
    public override void Register(IWindsorContainer container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines)
    {
      container.Register(
                         Component
                           .For<ISessionAccessor>()
                           .ImplementedBy<SessionAccessor>()
                           .LifestylePerWebRequest(),
                         Component
                           .For<IDocumentSession>()
                           .LifestyleTransient()
                           .UsingFactoryMethod((kernel, model, context) =>
                           {
                             var session = SessionFor(context.Handler.ComponentModel, kernel).OpenSession();

                             var sessions = kernel.Resolve<ISessionAccessor>();
                             sessions.Add(session);

                             return session;
                           }));
    }

    static IDocumentStore SessionFor(ComponentModel model, IKernel kernel)
    {
      var @namespace = model.Implementation.Namespace;
      var serviceName = @namespace.Split('.').Skip(1).First();

      return kernel.Resolve<IDocumentStore>(serviceName);
    }
  }
}
