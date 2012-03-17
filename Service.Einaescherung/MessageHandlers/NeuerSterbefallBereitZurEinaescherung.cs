using NServiceBus;

using Raven.Client;

using Service.Sterbefall.Contracts;

namespace Service.Einaescherung.MessageHandlers
{
  public class NeuerSterbefallBereitZurEinaescherung : IHandleMessages<BereitZurEinaescherung>
  {
    readonly IDocumentSession _db;

    public NeuerSterbefallBereitZurEinaescherung(IDocumentSession db)
    {
      _db = db;
    }

    public void Handle(BereitZurEinaescherung message)
    {
      _db.Store(new Einaescherung.Models.Sterbefall(message.SterbefallNummer));
    }
  }
}