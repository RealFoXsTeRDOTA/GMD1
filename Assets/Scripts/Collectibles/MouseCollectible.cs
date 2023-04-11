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
      var gameController = GameObject.FindGameObjectWithTag("GameController")
                                     .GetComponent<GameController>();
      gameController.IncreaseScore();
      Destroy(gameObject);
    }
  }
}
