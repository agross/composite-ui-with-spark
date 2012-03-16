using System.Linq;
using System.Web.Mvc.Html;

using Raven.Client;
using Raven.Client.Linq;

using Web.Modularity.Blocks;

namespace Service.Sterbefall.UI.Blocks
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
      var list = _db.Query<Sterbefall.Models.Sterbefall>().OrderBy(x => x.Name);
      Html.RenderPartial(@"Sterbefall\Liste", new Models.Liste { Sterbefaelle = list, Count = list.Count() });
    }
  }
}
