using System.Web.Mvc.Html;

using Service.Stock.Services;

using Web.Modularity.Blocks;

namespace Service.Stock.Blocks
{
  public class StockItemsForArticleView
  {
    public int Article { get; set; }
    public int NumberOfItems { get; set; }
  }

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
