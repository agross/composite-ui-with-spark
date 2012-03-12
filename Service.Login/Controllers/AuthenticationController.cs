using System.Web.Mvc;
using System.Web.Security;

using Service.Login.Services;

namespace Service.Login.Controllers
{
  public class AuthenticationController : Controller
  {
    readonly IAuthenticationService _authentication;

    public AuthenticationController(IAuthenticationService authentication)
    {
      _authentication = authentication;
    }

    [HttpPost]
    public ActionResult Login(string user, string password)
    {
      if (_authentication.Authenticate(user, password))
      {
        FormsAuthentication.SetAuthCookie(user, false);
      }
      return RedirectToAction("Index", "Home");
    }

    public ActionResult Logout()
    {
      FormsAuthentication.SignOut();
      return RedirectToAction("Index", "Home");
    }
  }
}
