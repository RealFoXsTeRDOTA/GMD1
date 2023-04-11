using UnityEngine;

public class EnemyDeathParticleSpawner : MonoBehaviour
{
  void Start()
  {
    GetComponent<ParticleSystem>().Play();
    Destroy(gameObject, 3f);
  }
}
