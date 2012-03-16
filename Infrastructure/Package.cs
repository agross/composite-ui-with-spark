﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

using Raven.Client;

using Web.Modularity;

namespace Infrastructure
{
  public class Package : WebPackage
  {
    public override void Register(IKernel container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines)
    {
      container.Register(Component
                           .For<IDocumentSession>()
                           .LifestylePerWebRequest()
                           .UsingFactoryMethod((kernel, model, context) =>
                                               SessionFor(context.Handler.ComponentModel, kernel).OpenSession())
                           .OnDestroy(session => session.SaveChanges()));
    }

    static IDocumentStore SessionFor(ComponentModel model, IKernel kernel)
    {
      var @namespace = model.Implementation.Namespace;
      var serviceName = @namespace.Split('.').Skip(1).First();

      return kernel.Resolve<IDocumentStore>(serviceName);
    }
  }
}
