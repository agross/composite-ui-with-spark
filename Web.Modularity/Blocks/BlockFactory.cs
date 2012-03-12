namespace Web.Modularity.Blocks
{
  public interface IBlockFactory
  {
    IBlock CreateBlock(string blockName, object argumentsForConstructor);
    void ReleaseBlock(IBlock block);
  }
}
