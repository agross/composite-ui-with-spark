using System;

namespace Service.Sterbefall.Models
{
  public class Sterbefall
  {
    public Sterbefall(string name)
    {
      Id = Guid.NewGuid();
      Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
  }
}
