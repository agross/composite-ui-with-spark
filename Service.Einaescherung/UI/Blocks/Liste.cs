using System;
using System.Linq;
using System.Web.Mvc.Html;

using Raven.Client;

using Web.Modularity.Blocks;

namespace Service.Einaescherung.UI.Blocks
{
  public class Liste : Block
  {
    readonly IDocumentSession _db;

    public Liste(IDocumentSession db)
    {
      _db = db;
    }

    protected override void RenderBlock()
    {
      var ravenQueryable = _db.Query<Einaescherung.Models.Sterbefall>().Customize(l => l.WaitForNonStaleResults()).ToList();
      var ids = ravenQueryable.Where(x => x.Einaescherungsdatum == null).Select(x => x.Id).ToList();
      Html.RenderPartial(@"Einaescherung\Liste", new Models.Liste { SterbefallNummern = ids, Count = ids.Count() });
    }
  }
}
