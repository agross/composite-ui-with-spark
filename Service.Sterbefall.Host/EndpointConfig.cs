using NServiceBus;

namespace Service.Sterbefall.Host
{
  public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
  {
    public void Init()
    {
      Configure.With()
        .Log4Net()
        .CastleWindsorBuilder()
        .XmlSerializer()
        .MsmqTransport()
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
