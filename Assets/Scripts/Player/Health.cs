using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
  [SerializeField]
  private GameObject healthContainer;
  private PlayerAnimation animationScript;
  private int health;
  private int maxHealth;
  private bool isHit;

  private void Start()
  {
    maxHealth = 9;
    health = maxHealth;
    animationScript = GetComponent<PlayerAnimation>();
  }

  private void Update()
  {
    var images = healthContainer.GetComponentsInChildren<Image>();
    for (var i = 0; i < maxHealth; i++)
    {
      images[i].enabled = i < health;
    }
  }

  public void TakeDamage(int damage)
  {
    if (!isHit)
    {
      health -= damage;
      if (health > 0)
      {
        StartCoroutine(BecomeTemporarilyInvincible());
      }
      else
      {
        animationScript.SetDeath(true);
      }
    }
  }

  public void GiveHealth(int health)
  {
    if (health < maxHealth)
    {
      this.health += health;
    }
  }

  private IEnumerator BecomeTemporarilyInvincible()
  {
    isHit = true;
    animationScript.SetHit(true);
    yield return new WaitForSeconds(0.25f);
    animationScript.SetHit(false);
    isHit = false;
  }
}
