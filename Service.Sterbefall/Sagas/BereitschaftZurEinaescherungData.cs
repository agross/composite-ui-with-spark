using System;

using NServiceBus.Saga;

namespace Service.Sterbefall.Sagas
{
  public class BereitschaftZurEinaescherungData : IContainSagaData
  {
    public bool WartezeitVergangen { get; set; }
    public bool PapiereVollständig { get; set; }
    public Guid SterbefallNummer { get; set; }

    public Guid Id { get; set; }
    public string Originator { get; set; }
    public string OriginalMessageId { get; set; }
  }
}
