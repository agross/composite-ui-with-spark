using System;

using NServiceBus;

namespace Service.Sterbefall.Sagas
{
  public class PapiereSindVollständig : IEvent
  {
    public Guid SterbefallNummer { get; set; }
  }
}