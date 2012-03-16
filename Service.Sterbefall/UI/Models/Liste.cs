using System.Collections.Generic;

namespace Service.Sterbefall.UI.Models
{
  public class Liste
  {
    public int Count { get; set; }

    public IEnumerable<Sterbefall.Models.Sterbefall> Sterbefaelle { get; set; }
  }
}