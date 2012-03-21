using System;
using System.Web.Mvc;

using Raven.Client;

namespace Service.Einaescherung.UI.Controllers
{
  public class EinaescherungController : Controller
  {
    readonly IDocumentSession _db;

    public EinaescherungController(IDocumentSession db)
    {
      _db = db;
    }

    public ActionResult Einaeschern(Guid sterbefallNummer)
    {
      var sf = _db.Load<Einaescherung.Models.Sterbefall>(sterbefallNummer);
      sf.Einaescherungsdatum = DateTime.Now;
      return RedirectToAction("Index", "Home");
    }
  }
}
