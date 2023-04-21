using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
  [SerializeField]
  private AudioClip collectiblePickUpSoundEffect;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      // TODO - Make audio manager, i.e. ONE single audio source throughout the game that has access to all sounds. Can then call methods to play specific sounds.
      AudioSource.PlayClipAtPoint(collectiblePickUpSoundEffect, transform.position);
      var health = collision.gameObject.GetComponent<Health>();
      health.GiveHealth(1);
      Destroy(gameObject);
    }
  }
}
