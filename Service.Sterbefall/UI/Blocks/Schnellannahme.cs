using System.Web.Mvc.Html;

using Web.Modularity.Blocks;

namespace Service.Sterbefall.UI.Blocks
{
  public class Schnellannahme : Block
  {
    protected override void RenderBlock()
    {
      Html.RenderPartial(@"Sterbefall\Schnellannahme");
    }
  }
}
