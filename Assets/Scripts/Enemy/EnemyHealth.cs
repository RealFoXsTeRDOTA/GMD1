using UnityEngine;

public class EnemyHealth : MonoBehaviour, IEnemyHealth
{
  [SerializeField]
  private float maxHealth = 5;
  private float currentHealth;
  private ParticleSystem particlesOnDamage;

  [SerializeField]
  private GameObject particlesOnDeath;

  [SerializeField]
  private AudioClip deathSoundEffect;

  private void Start()
  {
    particlesOnDamage = GetComponent<ParticleSystem>();
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
    FindFirstObjectByType<AudioManager>().Play(deathSoundEffect);
    Instantiate(particlesOnDeath, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
