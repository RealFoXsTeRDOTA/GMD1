namespace Tiles
{
  public interface IEnterExitTile
  {
    public void OnEnter(PlayerController playerController);
    public void OnExit(PlayerController playerController);
  }
}