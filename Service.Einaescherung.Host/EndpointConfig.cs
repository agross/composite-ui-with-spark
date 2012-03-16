using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Infrastructure;

using NServiceBus;
using NServiceBus.UnitOfWork;

using Raven.Client;
using Raven.Client.Document;

namespace Service.Einaescherung.Host
{
  public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
  {
    readonly IWindsorContainer _container = new WindsorContainer();

    public void Init()
    {
      var documentStore = new DocumentStore
                          {
                            Url = "http://localhost:8080/",
                            DefaultDatabase = "Einaescherung",
                            EnlistInDistributedTransactions = true
                          };
      documentStore.Initialize();

      _container.Register(Component
                            .For<IDocumentStore>()
                            .Instance(documentStore)
                            .Named("Application Store"),
                          Component
                            .For<IDocumentSession>()
                            .UsingFactoryMethod(kernel =>
                                                kernel.Resolve<IDocumentStore>("Application Store").OpenSession())
                            .Named("Application Session"),
                          Component
                            .For<IManageUnitsOfWork>()
                            .ImplementedBy<ApplicationRavenUnitOfWork>()
                            .Named("Application Unit Of Work")
                            .DependsOn(Dependency.OnComponent(typeof(IDocumentSession), "Application Session")));

      Configure.With()
        .Log4Net()
        .CastleWindsorBuilder(_container)
        .XmlSerializer()
        .MsmqTransport()
        .MsmqSubscriptionStorage()
        .UnicastBus()
        .LoadMessageHandlers()
        .Sagas()
        .RavenPersistence()
        .RunTimeoutManager()
        .CreateBus()
        .Start();
    }
  }
}
