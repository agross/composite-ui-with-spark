using System.Collections.Generic;

namespace Service.Sterbefall.Persistence
{
  class InMemorySterbefallRepository : ISterbefallRepository
  {
    static readonly IList<Models.Sterbefall> List = new List<Models.Sterbefall>();

    public void Insert(Models.Sterbefall sterbefall)
    {
      List.Add(sterbefall);
    }
  }
}