using System.Collections;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
  [SerializeField]
  private int damageOnHit = 1;

  [SerializeField]
  private float secondsBetweenDamage = 1f;
  private bool isTargetOnDamageZone = false;

  private IEnumerator DamagePlayer(Health healthComponent)
  {
    while (isTargetOnDamageZone)
    {
      healthComponent.TakeDamage(damageOnHit);
      yield return new WaitForSeconds(secondsBetweenDamage);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.CompareTag("Player") || !collision.TryGetComponent<Health>(out var healthComponent))
    {
      return;
    }

    isTargetOnDamageZone = true;
    StartCoroutine(DamagePlayer(healthComponent));
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (!collision.CompareTag("Player"))
    {
      return;
    }

    isTargetOnDamageZone = false;
  }
}
