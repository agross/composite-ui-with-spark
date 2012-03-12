using System;

namespace Web.Modularity
{
  static class ComponentNamingExtensions
  {
    internal static string ScopedTo(this string component, string service)
    {
      if (service == null)
      {
        return component;
      }

      return String.Format("{0}/{1}", service, component);
    }

    internal static string ScopedTo(this Type componentType, string service)
    {
      if (service == null)
      {
        return componentType.Name;
      }

      return String.Format("{0}/{1}", service, componentType.Name);
    }
  }
}