using System;
using UnityEngine;

public class LavaTile : MonoBehaviour, ITile
{
  [SerializeField]
  private GameObject fireParticleSystem;

  private ParticleSystem particleSystem;

  private void Start()
  {
    particleSystem = fireParticleSystem.GetComponent<ParticleSystem>();
    particleSystem.Stop();
  }

  public void OnEnter(PlayerController collision)
  {
    var health = collision.gameObject.GetComponent<Health>();
    // GameObject fireParticleSystem = collision.fireParticleSystem;
    if (health != null)
    {
      health.TakeDamage(1);
    }
    particleSystem.Play();
  }

  public void OnExit(PlayerController collision)
  {
    // GameObject fireParticleSystem = collision.fireParticleSystem;
    particleSystem.Stop();
    
  }

  public void onStay(PlayerController collision)
  {
    fireParticleSystem.transform.position = collision.transform.position;
  }
}
