using System;

using NServiceBus;

namespace Service.Sterbefall.Contracts
{
  public class BereitZurEinaescherung : IEvent
  {
    public Guid SterbefallNummer { get; set; }
  }
}