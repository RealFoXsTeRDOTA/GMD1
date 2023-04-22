using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
  [SerializeField]
  private AudioClip collectiblePickUpSoundEffect;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      FindFirstObjectByType<AudioManager>().Play(collectiblePickUpSoundEffect);
      var health = collision.gameObject.GetComponent<Health>();
      health.GiveHealth(1);
      Destroy(gameObject);
    }
  }
}
