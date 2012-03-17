using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

using NServiceBus;

namespace Service.Einaescherung.Host
{
  public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
  {
    readonly IWindsorContainer _container = new WindsorContainer();

    public void Init()
    {
      _container.Install(FromAssembly.InDirectory(new AssemblyFilter(".", "Service.*")));

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
