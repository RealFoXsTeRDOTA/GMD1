using UnityEngine;

public class MouseCollectible : MonoBehaviour
{
  [SerializeField]
  private AudioClip collectiblePickUpSoundEffect;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      AudioSource.PlayClipAtPoint(collectiblePickUpSoundEffect, transform.position);
      var gameController = collision.GetComponent<GameController>();
      gameController.IncreaseScore();
      Destroy(gameObject);
    }
  }
}
