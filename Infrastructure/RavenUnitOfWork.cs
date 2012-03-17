using System;

using NServiceBus.UnitOfWork;

using Raven.Client;

namespace Infrastructure
{
  public class RavenUnitOfWork : IManageUnitsOfWork
  {
    readonly IDocumentSession _session;

    public RavenUnitOfWork(IDocumentSession session)
    {
      _session = session;
    }

    public void Begin()
    {
    }

    public void End(Exception ex)
    {
      if (ex != null)
      {
        return;
      }
      _session.SaveChanges();
    }
  }
}