using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

using Castle.Core;
using Castle.MicroKernel;

using Web.Modularity.Blocks;

namespace Web.Modularity
{
  public class ModularControllerFactory : IControllerFactory, IBlockFactory
  {
    readonly IKernel _kernel;

    public ModularControllerFactory(IKernel kernel)
    {
      _kernel = kernel;
    }

    public IBlock CreateBlock(string blockName, object argumentsForConstructor)
    {
      var key = blockName.ToLowerInvariant();
      return _kernel.Resolve<IBlock>(key, new ReflectionBasedDictionaryAdapter(argumentsForConstructor));
    }

    public void ReleaseBlock(IBlock block)
    {
      _kernel.ReleaseComponent(block);
    }

    public IController CreateController(RequestContext requestContext, string controllerName)
    {
      var controllerKey = controllerName + "Controller";

      object service;
      if (requestContext.RouteData.Values.TryGetValue("service", out service))
      {
        var serviceControllerKey = controllerKey.ScopedTo(Convert.ToString(service));
        if (_kernel.HasComponent(serviceControllerKey))
        {
          return _kernel.Resolve<IController>(serviceControllerKey);
        }
      }

      return _kernel.HasComponent(controllerKey) ? _kernel.Resolve<IController>(controllerKey) : null;
    }

    public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
    {
      return SessionStateBehavior.Default;
    }

    public void ReleaseController(IController controller)
    {
      _kernel.ReleaseComponent(controller);
    }
  }
}
