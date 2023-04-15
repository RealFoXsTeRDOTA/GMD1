using Tiles;
using UnityEngine;

public class SlipperyTile : MonoBehaviour, IEnterExitTile
{
  public void OnEnter(PlayerController playerController)
  {
    playerController.IsSlipperyMovement = true;
  }

  public void OnExit(PlayerController playerController)
  {
    playerController.IsSlipperyMovement = false;
  }
}
