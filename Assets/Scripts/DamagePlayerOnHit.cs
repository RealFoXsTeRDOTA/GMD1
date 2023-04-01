using UnityEngine;

public class DamagePlayerOnHit : MonoBehaviour
{
  [SerializeField]
  private int DamageOnHit = 1;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.CompareTag("Player"))
    {
      return;
    }

    var healthComponent = collision.GetComponent<Health>();
    healthComponent.TakeDamage(DamageOnHit);
  }
}
