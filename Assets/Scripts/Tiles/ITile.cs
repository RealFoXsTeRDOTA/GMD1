public interface ITile
{
  public void OnEnter(PlayerController collision);
  public void OnExit(PlayerController collision);
  public void onStay(PlayerController collision);
}
