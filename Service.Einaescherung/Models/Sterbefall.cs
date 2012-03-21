using System;

namespace Service.Einaescherung.Models
{
  public class Sterbefall
  {
    public Sterbefall(Guid id)
    {
      Id = id;
    }

    public Guid Id { get; set; }
    public DateTime? Einaescherungsdatum { get; set; }
  }
}
