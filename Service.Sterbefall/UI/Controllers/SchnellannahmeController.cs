using System.Web.Mvc;

using NServiceBus;

using Service.Sterbefall.Persistence;
using Service.Sterbefall.Sagas;

namespace Service.Sterbefall.UI.Controllers
{
  public class SchnellannahmeController : Controller
  {
    readonly IBus _bus;
    readonly ISterbefallRepository _db;

    public SchnellannahmeController(ISterbefallRepository db, IBus bus)
    {
      _db = db;
      _bus = bus;
    }

    public ActionResult Post(string name)
    {
      var sterbefall = new Models.Sterbefall(name);
      _db.Insert(sterbefall);

      _bus.Publish(new SterbefallEingegangen { SterbefallNummer = sterbefall.Id });
      return RedirectToAction("Index", "Home");
    }
  }
}
