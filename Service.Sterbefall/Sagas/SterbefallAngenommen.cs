using System;

using NServiceBus;

namespace Service.Sterbefall.Sagas
{
  public class SterbefallAngenommen : IEvent
  {
    public Guid SterbefallNummer { get; set; }
  }
}