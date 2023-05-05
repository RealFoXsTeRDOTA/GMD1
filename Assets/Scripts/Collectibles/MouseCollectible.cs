using UnityEngine;

public class MouseCollectible : MonoBehaviour
{
  [SerializeField]
  private AudioClip collectiblePickUpSoundEffect;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      FindFirstObjectByType<AudioManager>().Play(collectiblePickUpSoundEffect);
      FindFirstObjectByType<GameController>().IncreaseScore();
      Destroy(gameObject);
    }
  }
}
