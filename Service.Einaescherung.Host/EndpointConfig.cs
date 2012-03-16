﻿using NServiceBus;

namespace Service.Einaescherung.Host
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
