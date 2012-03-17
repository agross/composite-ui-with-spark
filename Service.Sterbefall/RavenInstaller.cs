using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Raven.Client;
using Raven.Client.Document;

namespace Service.Sterbefall
{
  public class RavenInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(Component
                           .For<IDocumentStore>()
                           .Instance(new DocumentStore
                                     {
                                       Url = "http://localhost:8080/",
                                       DefaultDatabase = PackageInfo.ServiceName,
                                       EnlistInDistributedTransactions = true
                                     }.Initialize())
                           .Named(PackageInfo.ServiceName));
    }
  }
}