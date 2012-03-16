using System.Web.Mvc.Html;

using Service.Stock.Services;

using Web.Modularity.Blocks;

namespace Service.Stock.UI.Blocks
{
  public class StockItems : Block
  {
    readonly IStockService _stockService;

    public StockItems(IStockService stockService)
    {
      _stockService = stockService;
    }

    protected override void RenderBlock()
    {
      Html.RenderPartial(@"Stock\StockItems", _stockService.GetNumberOfItemsInStock());
    }
  }
}
