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
  [SerializeField]
  private float invulnerabilityTimeInSeconds = .5f;

  [Header("SFX")]
  [SerializeField]
  private AudioClip damageSoundEffect;

  [SerializeField]
  private AudioClip deathSoundEffect;
  private AudioSource audioSource;

  private GameController gameController;

  private void Start()
  {
    maxHealth = 9;
    health = maxHealth;
    animationScript = GetComponent<PlayerAnimation>();

    // TODO - Make audio manager, i.e. ONE single audio source throughout the game that has access to all sounds. Can then call methods to play specific sounds.
    audioSource = GetComponent<AudioSource>();
    gameController = GameObject.FindGameObjectWithTag("GameController")
                               .GetComponent<GameController>();
  }

  // TODO - Find a better way of doing this? Calling GetComponentsInChildren every frame isn't great... Should probably also be placed in the game controller now too
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
        audioSource.PlayOneShot(damageSoundEffect);
        StartCoroutine(BecomeTemporarilyInvincible());
      }
      else
      {
        audioSource.PlayOneShot(deathSoundEffect);
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
    yield return new WaitForSeconds(invulnerabilityTimeInSeconds);
    animationScript.SetHit(false);
    isHit = false;
  }
}
