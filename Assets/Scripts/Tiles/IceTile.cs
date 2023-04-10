using System;
using Tiles;
using UnityEngine;

public class IceTile : MonoBehaviour, IEnterExitTile
{
  public void OnEnter(PlayerController playerController)
  {
    Rigidbody2D playerBody = playerController.GetComponent<Rigidbody2D>();
    playerController.SetIsSlipperyMovement(true);
    playerBody.drag = 0f;
  }

  public void OnExit(PlayerController playerController)
  {
    Rigidbody2D playerBody = playerController.GetComponent<Rigidbody2D>();
    playerController.SetIsSlipperyMovement(false);
    playerBody.drag = 4f;
  }
}
