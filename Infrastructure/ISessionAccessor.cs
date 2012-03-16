using System;

using Raven.Client;

namespace Infrastructure
{
  interface ISessionAccessor
  {
    void Add(IDocumentSession session);
    void Each(Action<IDocumentSession> proc);
  }
}