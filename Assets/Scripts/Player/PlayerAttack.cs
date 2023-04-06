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

  private void Start()
  {
    input.AttackEvent += HandleAttack;
    animator = GetComponent<Animator>();
  }

  private void HandleAttack()
  {
    if (Time.time < timeSinceLastAttack)
    {
      // TODO - Play cooldown sound?
      return;
    }

    animator.SetTrigger("Attack");
    var enemyCollidersHit = Physics2D.OverlapCircleAll(pointOfAttack.position, attackArea, enemyLayer);
    timeSinceLastAttack = Time.time + secondsPerAttack;

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
