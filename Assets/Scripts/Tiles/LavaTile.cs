using System.Collections;
using Tiles;
using UnityEngine;

public class LavaTile : MonoBehaviour, IEnterExitTile, IStayTile
{
  private ParticleSystem fireParticleSystem;
  private bool floorIsLava;

  [SerializeField]
  private float damageInterval = 1;

  private void Start()
  {
    fireParticleSystem = GetComponentInChildren<ParticleSystem>();
  }

  public void OnEnter(PlayerController playerController)
  {
    fireParticleSystem.Play();
    floorIsLava = true;

    if (playerController.TryGetComponent<Health>(out var healthComponent))
    {
      StartCoroutine(DamageOverTime(healthComponent));
    }
  }

  public void OnExit(PlayerController playerController)
  {
    floorIsLava = false;
    fireParticleSystem?.Stop();
  }

  public void OnStay(PlayerController playerController) {
    if (fireParticleSystem != null)
      fireParticleSystem.transform.position = playerController.transform.position;
  }

  private IEnumerator DamageOverTime(Health health)
  {
    while (floorIsLava)
    {
      health.TakeDamage(1);
      yield return new WaitForSeconds(damageInterval);
    }
  }
}
