using UnityEngine;

public class MouseCollectible : MonoBehaviour
{
  [SerializeField]
  private AudioClip collectiblePickUpSoundEffect;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      // TODO - Make audio manager, i.e. ONE single audio source throughout the game that has access to all sounds. Can then call methods to play specific sounds.
      AudioSource.PlayClipAtPoint(collectiblePickUpSoundEffect, transform.position);
      var gameController = GameObject.FindGameObjectWithTag("GameController")
                                     .GetComponent<GameController>();
      gameController.IncreaseScore();
      Destroy(gameObject);
    }
  }
}
