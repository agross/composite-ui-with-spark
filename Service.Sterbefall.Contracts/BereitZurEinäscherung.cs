using System;

using NServiceBus;

namespace Service.Sterbefall.Contracts
{
  public class BereitZurEin�scherung : IEvent
  {
    public Guid SterbefallNummer { get; set; }
  }
}