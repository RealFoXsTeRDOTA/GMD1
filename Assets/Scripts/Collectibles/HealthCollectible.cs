using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      var health = collision.gameObject.GetComponent<Health>();
      health.GiveHealth(1);
      gameObject.SetActive(false);
    }
  }
}
