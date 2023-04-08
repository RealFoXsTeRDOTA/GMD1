using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  [SerializeField]
  private float maxHealth = 5;
  private float currentHealth;

  [SerializeField]
  private ParticleSystem particlesOnDamage;

  [SerializeField]
  private Transform particlesOnDeath;

  private void Start()
  {
    currentHealth = maxHealth;
  }

  public void TakeDamage(int damage)
  {
    currentHealth -= damage;

    if (currentHealth <= 0)
    {
      Kill();
    }
    else
    {
      particlesOnDamage.Play();
    }
  }

  private void Kill()
  {
    var spawnPosition = new Vector3(transform.position.x, transform.position.y - .4f, transform.position.z);
    Instantiate(particlesOnDeath, spawnPosition, Quaternion.identity);
    Destroy(gameObject);
  }
}
