using System.Collections;
using Tiles;
using UnityEngine;

public class LavaTile : MonoBehaviour, IEnterExitTile, IStayTile
{
  private ParticleSystem fireParticleSystem;
  private bool floorIsLava;

  private void Start()
  {
    fireParticleSystem = GetComponentInChildren<ParticleSystem>();
    if (fireParticleSystem != null)
    {
      fireParticleSystem.Stop();
    }
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
      yield return new WaitForSeconds(2f);
    }
  }
}
