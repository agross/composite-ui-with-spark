using System;
using System.Web.Mvc.Html;

using Raven.Client;

using Web.Modularity.Blocks;

namespace Service.Einaescherung.UI.Blocks
{
  public class Datum : Block
  {
    readonly IDocumentSession _db;
    readonly Guid _sterbefallNummer;

    public Datum(IDocumentSession db, Guid sterbefallNummer)
    {
      _db = db;
      _sterbefallNummer = sterbefallNummer;
    }

    protected override void RenderBlock()
    {
      var sterbefall = _db.Load<Einaescherung.Models.Sterbefall>(_sterbefallNummer);
      if (sterbefall != null && sterbefall.Einaescherungsdatum.HasValue)
      {
        Html.RenderPartial(@"Einaescherung\Datum", sterbefall.Einaescherungsdatum.Value);
      }
    }
  }
}
