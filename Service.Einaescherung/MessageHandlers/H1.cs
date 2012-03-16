using NServiceBus;

using Raven.Client;

using Service.Sterbefall.Contracts;

namespace Service.Einaescherung.MessageHandlers
{
  public class H1 : IHandleMessages<BereitZurEinäscherung>
  {
    readonly IDocumentSession _db;

    public H1(IDocumentSession db)
    {
      _db = db;
    }

    public void Handle(BereitZurEinäscherung message)
    {
      _db.Store(new Models.Sterbefall(message.SterbefallNummer));
    }
  }
}