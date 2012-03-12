using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using Spark.Bindings;

namespace Web.Modularity.Bindings
{
  [DebuggerDisplay("Bindings for {_service} service")]
  public class ServiceBindingProvider : BindingProvider
  {
    readonly string _service;

    public ServiceBindingProvider(string service)
    {
      _service = service;
    }

    public override IEnumerable<Binding> GetBindings(BindingRequest bindingRequest)
    {
      if (!bindingRequest.ViewFolder.HasView(_service + "/Bindings.xml"))
      {
        return Enumerable.Empty<Binding>();
      }

      var file = bindingRequest.ViewFolder.GetViewSource(_service + "/Bindings.xml");
      using (var stream = file.OpenViewStream())
      {
        using (var reader = new StreamReader(stream))
        {
          return LoadStandardMarkup(reader);
        }
      }
    }
  }
}
