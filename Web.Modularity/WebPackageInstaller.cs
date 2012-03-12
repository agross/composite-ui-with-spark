using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Spark;
using Spark.Bindings;
using Spark.Web.Mvc;

using Web.Modularity.Bindings;

namespace Web.Modularity
{
  public class WebPackageInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

      container
        .Register(Component.For<IControllerFactory>()
                    .ImplementedBy<ModularControllerFactory>()
                    .LifeStyle.Singleton);

      SetUpSparkViewEngine(container);
      RegisterPackages(container.Kernel, RouteTable.Routes, ViewEngines.Engines);
    }

    static void SetUpSparkViewEngine(IWindsorContainer container)
    {
      container.Register(Component.For<IBindingProvider>().ImplementedBy<DefaultBindingProvider>());


      var settings = new SparkSettings();
      settings.SetAutomaticEncoding(true);

      var services = SparkEngineStarter.CreateContainer(settings);
      services.SetService<IBindingProvider>(new CompositeBindingProvider(container));

      SparkEngineStarter.RegisterViewEngine(services);
    }

    void LocatePackages(IKernel kernel)
    {
      var searchPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                    AppDomain.CurrentDomain.RelativeSearchPath);

      foreach (var assembly in Directory.GetFiles(searchPath, "*.dll").Select(Path.GetFileNameWithoutExtension))
      {
        kernel.Register(Classes.FromAssemblyNamed(assembly)
                          .BasedOn<IWebPackage>()
                          .WithService.FromInterface(typeof(IWebPackage))
                          .Configure(c => c.Named(c.Implementation.FullName)));
      }
    }

    void RegisterPackages(IKernel kernel, ICollection<RouteBase> routes, ICollection<IViewEngine> engines)
    {
      LocatePackages(kernel);

      var remainingPackages = kernel.GetHandlers(typeof(IWebPackage));

      while (remainingPackages.Any())
      {
        var validPackages = remainingPackages
          .Where(handler => handler.CurrentState == HandlerState.Valid)
          .ToArray();

        if (!validPackages.Any())
        {
          break;
        }

        foreach (var handler in validPackages)
        {
          var package = kernel.Resolve<IWebPackage>(handler.ComponentModel.Name);
          package.Register(kernel, routes, engines);
          kernel.ReleaseComponent(package);
        }

        remainingPackages = remainingPackages.Except(validPackages).ToArray();
      }

      if (!remainingPackages.Any())
      {
        return;
      }

      var message =
        String.Format(
                      "Web packages have unresolved dependencies. The following packages could not be installed: {0}",
                      String.Join(", ", remainingPackages.Select(x => x.ComponentModel.Implementation)));

      throw new ApplicationException(message);
    }
  }
}
