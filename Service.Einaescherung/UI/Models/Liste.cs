using System;
using System.Collections.Generic;

namespace Service.Einaescherung.UI.Models
{
  public class Liste
  {
    public int Count { get; set; }

    public IEnumerable<Guid> SterbefallNummern { get; set; }
  }
}
