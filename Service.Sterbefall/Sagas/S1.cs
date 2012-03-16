using System;

using NServiceBus;
using NServiceBus.Saga;

using Service.Sterbefall.Contracts;

namespace Service.Sterbefall.Sagas
{
  public class S1 : Saga<S1Data>,
                    ISagaStartedBy<NehmeSterbefallAn>,
                    IHandleMessages<PapiereSindVollständig>,
                    IHandleMessages<SterbedatumHinterlegt>,
                    IHandleTimeouts<Wiedervorlage>
  {
    public void Handle(PapiereSindVollständig message)
    {
      Data.PapiereVollständig = true;

      if (Data.ZweiTageVergangen)
      {
        Bus.Publish(new BereitZurEinäscherung { SterbefallNummer = message.SterbefallNummer });
        MarkAsComplete();
      }
    }

    public void Handle(SterbedatumHinterlegt message)
    {
      var noNeedToWait = DateTime.Now.Subtract(message.Sterbedatum) > TimeSpan.FromDays(2);
      if (noNeedToWait && Data.PapiereVollständig)
      {
        Bus.Publish(new BereitZurEinäscherung { SterbefallNummer = message.SterbefallNummer });
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
        Bus.Publish(new BereitZurEinäscherung { SterbefallNummer = state.SterbefallNummer });
        MarkAsComplete();
        return;
      }

      Data.ZweiTageVergangen = true;
    }

    public void Handle(NehmeSterbefallAn message)
    {
      Data.SterbefallNummer = message.SterbefallNummer;
    }

    public override void ConfigureHowToFindSaga()
    {
      ConfigureMapping<NehmeSterbefallAn>(s => s.SterbefallNummer, m => m.SterbefallNummer);
      ConfigureMapping<PapiereSindVollständig>(s => s.SterbefallNummer, m => m.SterbefallNummer);
      ConfigureMapping<SterbedatumHinterlegt>(s => s.SterbefallNummer, m => m.SterbefallNummer);
      ConfigureMapping<Wiedervorlage>(s => s.SterbefallNummer, m => m.SterbefallNummer);
    }
  }
}
