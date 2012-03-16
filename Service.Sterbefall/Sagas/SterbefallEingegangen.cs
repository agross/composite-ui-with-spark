using System;

using NServiceBus;

namespace Service.Sterbefall.Sagas
{
  public class SterbefallEingegangen : IEvent
  {
    public Guid SterbefallNummer { get; set; }
  }
}