using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  [SerializeField]
  private float maxHealth = 2;
  private float currentHealth;

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
  }

  private void Kill()
  {
    Destroy(gameObject);
  }
}
