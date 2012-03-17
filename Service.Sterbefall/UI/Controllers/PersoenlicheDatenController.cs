using System;
using System.Web.Mvc;

using NServiceBus;

using Raven.Client;

using Service.Sterbefall.Sagas;

namespace Service.Sterbefall.UI.Controllers
{
  public class PersoenlicheDatenController: Controller
  {
    readonly IBus _bus;
    readonly IDocumentSession _db;

    public PersoenlicheDatenController(IDocumentSession db, IBus bus)
    {
      _db = db;
      _bus = bus;
    }

    public ActionResult SetSterbedatum(Guid sterbefallNummer)
    {
      var sterbefall = _db.Load<Sterbefall.Models.Sterbefall>(sterbefallNummer);
      sterbefall.Sterbedatum = DateTime.Now.Subtract(TimeSpan.FromDays(2));

      _bus.Publish(new SterbedatumHinterlegt { SterbefallNummer = sterbefallNummer, Sterbedatum = sterbefall.Sterbedatum.Value });
      return RedirectToAction("Index", "Home");
    }

    public ActionResult SetPapiereVollstaendig(Guid sterbefallNummer)
    {
      var sterbefall = _db.Load<Sterbefall.Models.Sterbefall>(sterbefallNummer);
      sterbefall.PapiereVollstaendig = true;

      _bus.Publish(new PapiereSindVollstaendig { SterbefallNummer = sterbefallNummer});
      return RedirectToAction("Index", "Home");
    }
  }
}