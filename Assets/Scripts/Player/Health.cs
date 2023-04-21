using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
  private PlayerAnimation animationScript;
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
    animationScript = GetComponent<PlayerAnimation>();

    // TODO - Make audio manager, i.e. ONE single audio source throughout the game that has access to all sounds. Can then call methods to play specific sounds.
    audioSource = GetComponent<AudioSource>();
    gameController = GameObject.FindGameObjectWithTag("GameController")
                               .GetComponent<GameController>();

    gameController.PlayerDeathEvent += HandlePlayerDeath;
  }

  public void TakeDamage(int damage)
  {
    if (!isHit && gameController.CurrentPlayerHealth > 0)
    {
      gameController.TakeDamage(damage);

      if (gameController.CurrentPlayerHealth > 0)
      {
        audioSource.PlayOneShot(damageSoundEffect);
        StartCoroutine(BecomeTemporarilyInvincible());
      }
    }
  }

  private void HandlePlayerDeath()
  {
    audioSource.PlayOneShot(deathSoundEffect);
    animationScript.SetDeath(true);
  }

  public void GiveHealth(int health)
  {
    if (health < gameController.MaxPlayerHealth)
    {
      gameController.GiveHealth(health);
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

  private void OnDestroy()
  {
    gameController.PlayerDeathEvent -= HandlePlayerDeath;
  }
}
