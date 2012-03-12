using System.Web.Mvc;

namespace Web.Modularity.Blocks
{
  public interface IBlock
  {
    void RenderBlock(ViewContext viewContext);
  }

  public abstract class Block : IBlock, IViewDataContainer
  {
    public ViewContext ViewContext { get; set; }
    public HtmlHelper Html { get; set; }
    public AjaxHelper Ajax { get; set; }
    public UrlHelper Url { get; set; }

    void IBlock.RenderBlock(ViewContext viewContext)
    {
      ViewData = viewContext.ViewData;
      ViewContext = viewContext;
      Html = new HtmlHelper(viewContext, this);
      Ajax = new AjaxHelper(viewContext, this);
      Url = new UrlHelper(viewContext.RequestContext);

      RenderBlock();
    }

    public ViewDataDictionary ViewData { get; set; }

    protected abstract void RenderBlock();
  }
}
