using System;
using System.Web.Mvc.Html;

using Raven.Client;

using Web.Modularity.Blocks;

namespace Service.Sterbefall.UI.Blocks
{
  public class Name : Block
  {
    readonly IDocumentSession _db;
    readonly Guid _sterbefallNummer;

    public Name(IDocumentSession db, Guid sterbefallNummer)
    {
      _db = db;
      _sterbefallNummer = sterbefallNummer;
    }

    protected override void RenderBlock()
    {
      var sterbefall = _db.Load<Sterbefall.Models.Sterbefall>(_sterbefallNummer);
      Html.RenderPartial(@"Sterbefall\Name", sterbefall.Name);
    }
  }
}