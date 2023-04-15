using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private Transform pointOfAttack;

  [SerializeField]
  private float attackArea = .4f;

  [SerializeField]
  private int attackDamage = 1;

  [SerializeField]
  private float secondsPerAttack = .5f;
  private float timeSinceLastAttack = 0f;

  [SerializeField]
  private LayerMask enemyLayer;

  [Header("SFX")]
  private AudioSource audioSource;

  [SerializeField]
  private AudioClip attackSoundEffect;

  [SerializeField]
  private AudioClip attackCooldownSoundEffect;
  private SpriteRenderer attackSpriteRenderer;

  private void Start()
  {
    input.AttackEvent += HandleAttack;
    audioSource = GetComponent<AudioSource>();
    attackSpriteRenderer = pointOfAttack.GetComponent<SpriteRenderer>();
  }

  private void HandleAttack()
  {
    if (Time.time < timeSinceLastAttack)
    {
      audioSource.PlayOneShot(attackCooldownSoundEffect);
      return;
    }

    StartCoroutine(PlayAttackAnimation());
    var enemyCollidersHit = Physics2D.OverlapCircleAll(pointOfAttack.position, attackArea, enemyLayer);
    timeSinceLastAttack = Time.time + secondsPerAttack;
    audioSource.PlayOneShot(attackSoundEffect);

    foreach (var collider in enemyCollidersHit)
    {
      if (collider.isTrigger)
      {
        continue;
      }

      var healthComponent = collider.GetComponent<EnemyHealth>();
      healthComponent.TakeDamage(attackDamage);
    }
  }

  private IEnumerator PlayAttackAnimation()
  {
    attackSpriteRenderer.enabled = true;
    yield return new WaitForSeconds(.1f);
    attackSpriteRenderer.enabled = false;
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(pointOfAttack.position, attackArea);
  }

  private void OnDestroy()
  {
    input.AttackEvent -= HandleAttack;
  }
}
