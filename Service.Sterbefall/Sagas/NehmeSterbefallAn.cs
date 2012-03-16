using System;

using NServiceBus;

namespace Service.Sterbefall.Sagas
{
  public class NehmeSterbefallAn : ICommand
  {
    public Guid SterbefallNummer { get; set; }
  }
}