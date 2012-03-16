using System;

using NServiceBus;

namespace Service.Sterbefall.Contracts
{
  public class BereitZurEinäscherung : IEvent
  {
    public Guid SterbefallNummer { get; set; }
  }
}