using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using Raven.Client;

namespace Infrastructure
{
  class SessionAccessor : ISessionAccessor
  {
    readonly IDictionary<IDocumentSession, object> _sessions = new ConcurrentDictionary<IDocumentSession, object>();

    public void Add(IDocumentSession session)
    {
      _sessions.Add(session, null);
    }

    public void Each(Action<IDocumentSession> proc)
    {
      _sessions.Keys.ToList().ForEach(proc);
    }
  }
}