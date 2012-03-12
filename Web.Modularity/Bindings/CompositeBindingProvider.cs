using System.Collections.Generic;
using System.Linq;

using Castle.Windsor;

using Spark.Bindings;

namespace Web.Modularity.Bindings
{
  public class CompositeBindingProvider : IBindingProvider
  {
    readonly IWindsorContainer _container;

    public CompositeBindingProvider(IWindsorContainer container)
    {
      _container = container;
    }

    public IEnumerable<Binding> GetBindings(BindingRequest bindingRequest)
    {
      var providers = _container.ResolveAll<IBindingProvider>();
      var bindings = providers.SelectMany(x => x.GetBindings(bindingRequest));
      providers.ToList().ForEach(x => _container.Release(x));

      return bindings;
    }
  }
}
