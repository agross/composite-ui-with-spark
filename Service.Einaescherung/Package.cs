using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

using Raven.Client;
using Raven.Client.Document;

using Web.Modularity;

namespace Service.Einaescherung
{
  public class Package : WebPackage
  {
    const string ServiceName = "Einaescherung";

    public override void Register(IKernel container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines)
    {
      RegisterDefault(container, routes, viewEngines, ServiceName);

      var documentStore = new DocumentStore
                          {
                            Url = "http://localhost:8080/",
                            DefaultDatabase = ServiceName,
                            EnlistInDistributedTransactions = true
                          };
      documentStore.Initialize();

      container.Register(Component
                           .For<IDocumentStore>()
                           .Instance(documentStore)
                           .Named(ServiceName));
    }
  }
}
