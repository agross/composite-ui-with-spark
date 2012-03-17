using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Infrastructure;

using NServiceBus.UnitOfWork;

using Raven.Client;

namespace Service.Einaescherung.Host
{
  public class RavenInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(Component
                           .For<IDocumentSession>()
                           .LifestyleScoped()
                           .UsingFactoryMethod(kernel =>
                                               kernel.Resolve<IDocumentStore>(PackageInfo.ServiceName).OpenSession())
                           .Named("Application Session"),
                         Component
                           .For<IManageUnitsOfWork>()
                           .ImplementedBy<RavenUnitOfWork>()
                           .LifestyleScoped()
                           .Named("Application Unit Of Work")
                           .DependsOn(Dependency.OnComponent(typeof(IDocumentSession), "Application Session")));
    }
  }
}