using System.Web.Mvc.Html;

using Service.Stock.Services;
using Service.Stock.UI.Models;

using Web.Modularity.Blocks;

namespace Service.Stock.UI.Blocks
{
  public class StockItemsForArticle : Block
  {
    readonly int _articleNumber;
    readonly IStockService _stockService;

    public StockItemsForArticle(IStockService stockService, int articleNumber)
    {
      _stockService = stockService;
      _articleNumber = articleNumber;
    }

    protected override void RenderBlock()
    {
      Html.RenderPartial(@"Stock\StockItemsForArticle",
                         new StockItemsForArticleView
                         {
                           Article = _articleNumber,
                           NumberOfItems = _stockService.GetNumberOfItemsInStockForArticle(_articleNumber)
                         });
    }
  }
}
