using System.Web.Mvc.Html;

using Raven.Client;

using Web.Modularity.Blocks;

namespace Service.Sterbefall.UI.Blocks
{
  public class Name : Block
  {
    readonly IDocumentSession _db;
    readonly int _sterbefallNummer;

    public Name(IDocumentSession db, int sterbefallNummer)
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