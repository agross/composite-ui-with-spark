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
      Html.RenderPartial(@"Einaescherung\Liste", new Models.Liste { SterbefallNummern = Enumerable.Empty<Guid>(), Count = 0 });
    }
  }
}
