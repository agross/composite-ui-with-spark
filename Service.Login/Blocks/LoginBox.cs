using System.Web.Mvc.Html;

using Web.Modularity.Blocks;

namespace Service.Login.Blocks
{
  public class LoginBox : Block
  {
    protected override void RenderBlock()
    {
      if (ViewContext.HttpContext.Request.IsAuthenticated)
      {
        Html.RenderPartial(@"Login\LoggedIn", ViewContext.HttpContext.User.Identity.Name);
      }
      else
      {
        Html.RenderPartial(@"Login\LoginBox");
      }
    }
  }
}
