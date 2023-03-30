using UnityEngine;

public class MouseCollectible : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      var gameController = collision.GetComponent<GameController>();
      gameController.IncreaseScore();
      gameObject.SetActive(false);
    }
  }
}
