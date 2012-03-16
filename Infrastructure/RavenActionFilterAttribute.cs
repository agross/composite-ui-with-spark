using System.Web.Mvc;

using Castle.Windsor;

namespace Infrastructure
{
  public class RavenActionFilterAttribute : ActionFilterAttribute
  {
    readonly IWindsorContainer _container;

    public RavenActionFilterAttribute(IWindsorContainer container)
    {
      _container = container;
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
      if (filterContext.Exception != null)
      {
        return;
      }

      var sessions = _container.Resolve<ISessionAccessor>();
      sessions.Each(session => session.SaveChanges());
    }
  }
}
