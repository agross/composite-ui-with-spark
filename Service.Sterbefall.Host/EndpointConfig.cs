using NServiceBus;

namespace Service.Sterbefall.Host
{
  public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
  {
    public void Init()
    {
      Configure.With()
        .CastleWindsorBuilder()
        .XmlSerializer()
        .MsmqTransport()
        .MsmqSubscriptionStorage()
        .UnicastBus()
        .LoadMessageHandlers()
        .Sagas()
        .RavenSagaPersister()
        .RunTimeoutManager()
        .CreateBus()
        .Start();
    }
  }
}
