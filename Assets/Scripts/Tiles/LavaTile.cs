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
    StartCoroutine(DamageOverTime(playerController));
  }

  public void OnExit(PlayerController playerController)
  {
    floorIsLava = false;
    fireParticleSystem.Stop();
    
  }

  public void onStay(PlayerController playerController)
  {
    fireParticleSystem.transform.position = playerController.transform.position;
  }
  
  private IEnumerator DamageOverTime(PlayerController playerController)
  {
    while (floorIsLava)
    {
      var health = playerController.gameObject.GetComponent<Health>();
      if (health != null)
      {
        health.TakeDamage(1);
      }
      yield return new WaitForSeconds(2f);
    }
  }
}
