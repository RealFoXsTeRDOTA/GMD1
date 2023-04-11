using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private Transform pointOfAttack;

  [SerializeField]
  private float attackArea = .6f;

  [SerializeField]
  private int attackDamage = 1;

  [SerializeField]
  private float secondsPerAttack = .5f;
  private float timeSinceLastAttack = 0f;

  [SerializeField]
  private LayerMask enemyLayer;
  private Animator animator;

  [Header("SFX")]
  private AudioSource audioSource;

  [SerializeField]
  private AudioClip attackSoundEffect;

  [SerializeField]
  private AudioClip attackCooldownSoundEffect;

  private void Start()
  {
    input.AttackEvent += HandleAttack;
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
  }

  private void HandleAttack()
  {
    if (Time.time < timeSinceLastAttack)
    {
      audioSource.PlayOneShot(attackCooldownSoundEffect);
      return;
    }

    animator.SetTrigger("Attack");
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

  private void OnDrawGizmosSelected()
  {
    Gizmos.DrawWireSphere(pointOfAttack.position, attackArea);
  }

  private void OnDestroy()
  {
    input.AttackEvent -= HandleAttack;
  }
}
