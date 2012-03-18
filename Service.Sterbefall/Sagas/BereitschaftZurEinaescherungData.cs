using System;

using NServiceBus.Saga;

namespace Service.Sterbefall.Sagas
{
  public class BereitschaftZurEinaescherungData : IContainSagaData
  {
    public bool ZweiTageVergangen { get; set; }
    public bool PapiereVollst�ndig { get; set; }
    public Guid SterbefallNummer { get; set; }

    public Guid Id { get; set; }
    public string Originator { get; set; }
    public string OriginalMessageId { get; set; }
  }
}
