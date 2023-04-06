using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
  [SerializeField]
  private int damageOnHit = 1;

  [SerializeField]
  private float secondsBetweenDamage = 1f;
  private bool isPlayerOnSpikes = false;

  private IEnumerator DamagePlayer(Health healthComponent)
  {
    while (isPlayerOnSpikes)
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

    isPlayerOnSpikes = true;
    StartCoroutine(DamagePlayer(healthComponent));
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (!collision.CompareTag("Player"))
    {
      return;
    }

    isPlayerOnSpikes = false;
  }
}
