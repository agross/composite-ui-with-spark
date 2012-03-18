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
                                              IHandleTimeouts<Wiedervorlage>
  {
    public void Handle(SterbefallAngenommen message)
    {
      Data.SterbefallNummer = message.SterbefallNummer;
    }

    public void Handle(PapiereSindVollstaendig message)
    {
      Data.PapiereVollständig = true;

      if (Data.ZweiTageVergangen)
      {
        Bus.Publish(new BereitZurEinaescherung { SterbefallNummer = message.SterbefallNummer });
        MarkAsComplete();
      }
    }

    public void Handle(SterbedatumHinterlegt message)
    {
      var noNeedToWait = DateTime.Now.Subtract(message.Sterbedatum) > TimeSpan.FromDays(2);
      if (noNeedToWait && Data.PapiereVollständig)
      {
        Bus.Publish(new BereitZurEinaescherung { SterbefallNummer = message.SterbefallNummer });
        MarkAsComplete();
        return;
      }

      var wiedervorlageAm = message.Sterbedatum.AddDays(2);
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

      Data.ZweiTageVergangen = true;
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
