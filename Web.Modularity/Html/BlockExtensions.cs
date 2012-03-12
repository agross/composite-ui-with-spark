using System;
using System.Web.Mvc;

using Web.Modularity.Blocks;

namespace Web.Modularity.Html
{
  public static class BlockExtensions
  {
    public static void RenderBlock(this HtmlHelper helper, string blockName)
    {
      Render(blockName, helper.ViewContext, helper.ViewData);
    }

    public static void RenderBlock(this HtmlHelper helper, string blockName, object argumentsForConstructor)
    {
      Render(blockName, helper.ViewContext, argumentsForConstructor);
    }

    static void Render(string blockName, ViewContext viewContext, object argumentsForConstructor)
    {
      var blockFactory = BlockBuilder.GetBlockFactory();
      if (blockFactory == null)
      {
        throw new Exception("IBlockFactory not available from current controller factory");
      }

      var block = blockFactory.CreateBlock(blockName, argumentsForConstructor);
      block.RenderBlock(viewContext);
      blockFactory.ReleaseBlock(block);
    }
  }
}
