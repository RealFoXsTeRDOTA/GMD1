using UnityEngine;

public class SlimeDeathParticleSpawner : MonoBehaviour
{
  private ParticleSystem deathParticles;

  void Start()
  {
    deathParticles = GetComponent<ParticleSystem>();
    deathParticles.Play();
    Destroy(gameObject, 3f);
  }
}
