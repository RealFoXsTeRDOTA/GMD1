using UnityEngine;

public class LavaTile : MonoBehaviour, ITile
{
  public void OnEnter(PlayerController collision)
  {
    var health = collision.gameObject.GetComponent<Health>();
    GameObject fireParticleSystem = collision.fireParticleSystem;
    if (health != null)
    {
      Debug.Log("Take damage from lava");
      health.TakeDamage(1);
    }
    fireParticleSystem.SetActive(true);
  }

  public void OnExit(PlayerController collision)
  {
    GameObject fireParticleSystem = collision.fireParticleSystem;
    fireParticleSystem.SetActive(false);
  }
}
