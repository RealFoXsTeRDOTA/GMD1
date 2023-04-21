using UnityEngine;

public class EnemyHealth : MonoBehaviour
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
    // TODO - Make audio manager, i.e. ONE single audio source throughout the game that has access to all sounds. Can then call methods to play specific sounds.
    AudioSource.PlayClipAtPoint(deathSoundEffect, transform.position);
    Instantiate(particlesOnDeath, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }
}
