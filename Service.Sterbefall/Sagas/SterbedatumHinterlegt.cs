using System;

using NServiceBus;

namespace Service.Sterbefall.Sagas
{
  public class SterbedatumHinterlegt : IEvent
  {
    public Guid SterbefallNummer { get; set; }
    public DateTime Sterbedatum { get; set; }
  }
}