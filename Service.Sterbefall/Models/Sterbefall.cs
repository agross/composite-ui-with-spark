using System;

namespace Service.Sterbefall.Models
{
  public class Sterbefall
  {
    readonly Guid _id;

    public Sterbefall(string name)
    {
      _id = Guid.NewGuid();
      Name = name;
    }

    public string Name { get; set; }

    public Guid Id
    {
      get
      {
        return _id;
      }
    }
  }
}
