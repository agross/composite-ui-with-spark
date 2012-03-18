using System;

using NServiceBus;
using NServiceBus.Saga;

using Service.Sterbefall.Contracts;

namespace Service.Sterbefall.Sagas
{
  public class BereitschaftZurEinaescherung : Saga<BereitschaftZurEinaescherungData>,
                                              ISagaStartedBy<SterbefallAngenommen>,
                                              IHandleMessages<PapiereSindVollstaendig>,
                                              IHandleMessages<SterbedatumHinterlegt>,
                                              IHandleTimeouts<Wiedervorlage>,
                                              IWantCustomInitialization

  {
    public TimeSpan WartezeitVorFreigabe { get; set; }

    public void Handle(SterbefallAngenommen message)
    {
      Data.SterbefallNummer = message.SterbefallNummer;
    }

    public void Handle(PapiereSindVollstaendig message)
    {
      Data.PapiereVollständig = true;

      if (Data.WartezeitVergangen)
      {
        Bus.Publish(new BereitZurEinaescherung { SterbefallNummer = message.SterbefallNummer });
        MarkAsComplete();
      }
    }

    public void Handle(SterbedatumHinterlegt message)
    {
      var wartezeitVergangen = DateTime.Now.Subtract(message.Sterbedatum) > WartezeitVorFreigabe;
      if (wartezeitVergangen && Data.PapiereVollständig)
      {
        Bus.Publish(new BereitZurEinaescherung { SterbefallNummer = message.SterbefallNummer });
        MarkAsComplete();
        return;
      }

      var wiedervorlageAm = message.Sterbedatum.Add(WartezeitVorFreigabe);
      RequestUtcTimeout(wiedervorlageAm, new Wiedervorlage { SterbefallNummer = message.SterbefallNummer });
    }

    public void Timeout(Wiedervorlage state)
    {
      if (Data.PapiereVollständig)
      {
        Bus.Publish(new BereitZurEinaescherung { SterbefallNummer = state.SterbefallNummer });
        MarkAsComplete();
        return;
      }

      Data.WartezeitVergangen = true;
    }

    public void Init()
    {
      Configure.Instance.Configurer
        .ConfigureProperty<BereitschaftZurEinaescherung>(s => s.WartezeitVorFreigabe, TimeSpan.FromDays(2));
    }

    public override void ConfigureHowToFindSaga()
    {
      ConfigureMapping<SterbefallAngenommen>(s => s.SterbefallNummer, m => m.SterbefallNummer);
      ConfigureMapping<PapiereSindVollstaendig>(s => s.SterbefallNummer, m => m.SterbefallNummer);
      ConfigureMapping<SterbedatumHinterlegt>(s => s.SterbefallNummer, m => m.SterbefallNummer);
      ConfigureMapping<Wiedervorlage>(s => s.SterbefallNummer, m => m.SterbefallNummer);
    }
  }
}
