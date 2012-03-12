namespace Service.Stock.Services
{
  public interface IStockService
  {
    int GetNumberOfItemsInStock();
    int GetNumberOfItemsInStockForArticle(int articleNumber);
  }

  public class StockService : IStockService
  {
    public int GetNumberOfItemsInStock()
    {
      return 42;
    }

    public int GetNumberOfItemsInStockForArticle(int articleNumber)
    {
      return articleNumber;
    }
  }
}