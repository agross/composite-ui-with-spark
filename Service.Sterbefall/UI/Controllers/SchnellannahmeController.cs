using System.Web.Mvc;

using NServiceBus;

using Raven.Client;

using Service.Sterbefall.Sagas;

namespace Service.Sterbefall.UI.Controllers
{
  public class SchnellannahmeController : Controller
  {
    readonly IBus _bus;
    readonly IDocumentSession _db;

    public SchnellannahmeController(IDocumentSession db, IBus bus)
    {
      _db = db;
      _bus = bus;
    }

    public ActionResult Post(string name)
    {
      var sterbefall = new Sterbefall.Models.Sterbefall(name);
      _db.Store(sterbefall);

      _bus.Publish(new SterbefallAngenommen { SterbefallNummer = sterbefall.Id });
      return RedirectToAction("Index", "Home");
    }
  }
}
