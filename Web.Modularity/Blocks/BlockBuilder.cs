using System.Threading;
using System.Web.Mvc;

namespace Web.Modularity.Blocks
{
  class BlockBuilder
  {
    static BlockBuilder _current;

    public static BlockBuilder Current
    {
      get
      {
        return _current ?? Interlocked.CompareExchange(ref _current, new BlockBuilder(), null) ?? _current;
      }
    }

    public static IBlockFactory GetBlockFactory()
    {
      return ControllerBuilder.Current.GetControllerFactory() as IBlockFactory;
    }
  }
}
