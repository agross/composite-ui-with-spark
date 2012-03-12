using System.Web.Mvc.Html;

using Web.Modularity.Blocks;

namespace Service.Login.Blocks
{
  public class LoginBox : Block
  {
    protected override void RenderBlock()
    {
      Html.RenderPartial(@"Login\LoginBox");
    }
  }
}
