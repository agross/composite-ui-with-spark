using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;

using Raven.Client;
using Raven.Client.Document;

using Web.Modularity;

namespace Service.Sterbefall
{
  public class Package : WebPackage
  {
    public override void Register(IKernel container, ICollection<RouteBase> routes, ICollection<IViewEngine> viewEngines)
    {
      RegisterDefault(container, routes, viewEngines, "Sterbefall");

      var documentStore = new DocumentStore
                          {
                            Url = "http://localhost:8080/",
                            DefaultDatabase = "Service.Sterbefall",
                            EnlistInDistributedTransactions = true
                          };
      documentStore.Initialize();

      container.Register(Component
                           .For<IDocumentStore>()
                           .Instance(documentStore)
                           .Named("Sterbefall"));
    }
  }
}
