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
  private AudioManager audioManager;

  [SerializeField]
  private AudioClip attackSoundEffect;

  [SerializeField]
  private AudioClip attackCooldownSoundEffect;
  private SpriteRenderer attackSpriteRenderer;

  [SerializeField] private GameObject projectile;
  [SerializeField] private Transform projectileSpawner;
  [SerializeField] private float shootCooldown;
  private float shootTime;

  private void Start()
  {
    input.AttackEvent += HandleAttack;
    input.RangedAttackEvent += HandleRangedAttack;

    audioManager = FindFirstObjectByType<AudioManager>();
    attackSpriteRenderer = pointOfAttack.GetComponent<SpriteRenderer>();
  }

  private void Update()
  {
    shootTime += Time.deltaTime;
  }

  private void HandleAttack()
  {
    if (Time.time < timeSinceLastAttack)
    {
      audioManager.Play(attackCooldownSoundEffect);
      return;
    }

    StartCoroutine(PlayAttackAnimation());
    var enemyCollidersHit = Physics2D.OverlapCircleAll(pointOfAttack.position, attackArea, enemyLayer);
    timeSinceLastAttack = Time.time + secondsPerAttack;
    audioManager.Play(attackSoundEffect);

    foreach (var collider in enemyCollidersHit)
    {
      if (collider.isTrigger)
      {
        continue;
      }

      if (collider.TryGetComponent<EnemyHealth>(out var healthComponent))
      {
        healthComponent.TakeDamage(attackDamage);
      }
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
    input.RangedAttackEvent -= HandleRangedAttack;
  }

  /// <summary>
  /// only allow to shoot projectiles at an interval of time away from each other
  /// </summary>
  private void HandleRangedAttack()
  {
    if (shootTime >= shootCooldown)
    {
      FireProjectile();
      shootTime = 0;
    }
  }
  /// <summary>
  /// instantiate projectile at the position of a child transform on the player and the rotation of the player
  /// </summary>
  private void FireProjectile()
  {
    Instantiate(projectile, projectileSpawner.position, transform.rotation);
  }
}
