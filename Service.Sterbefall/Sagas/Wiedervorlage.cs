using System;

using NServiceBus.Saga;

namespace Service.Sterbefall.Sagas
{
  public class Wiedervorlage : ITimeoutState
  {
    public Guid SterbefallNummer { get; set; }
  }
}
