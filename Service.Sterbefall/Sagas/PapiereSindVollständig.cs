using System;

using NServiceBus;

namespace Service.Sterbefall.Sagas
{
  public class PapiereSindVollst�ndig : IEvent
  {
    public Guid SterbefallNummer { get; set; }
  }
}