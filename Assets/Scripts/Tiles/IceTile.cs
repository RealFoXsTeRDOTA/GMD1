using UnityEngine;

public class IceTile : MonoBehaviour, ITile
{
  public void OnEnter(PlayerController collision)
  {
    Rigidbody2D playerBody = collision.GetComponent<Rigidbody2D>();
    collision.isOnIce = true;
    playerBody.drag = 0f;
  }

  public void OnExit(PlayerController collision)
  {
    Rigidbody2D playerBody = collision.GetComponent<Rigidbody2D>();
    collision.isOnIce = false;
    playerBody.drag = 4f;
  }
}
