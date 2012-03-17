using System;

using NServiceBus;

namespace Service.Sterbefall.Sagas
{
  public class PapiereSindVollstaendig : IEvent
  {
    public Guid SterbefallNummer { get; set; }
  }
}