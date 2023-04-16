using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField] private InputReader input;

    [SerializeField] private Transform pointOfAttack;

    [SerializeField] private float attackArea = .6f;

    [SerializeField] private int attackDamage = 1;

    [SerializeField] private float secondsPerAttack = .5f;
    private float timeSinceLastAttack = 0f;

    [SerializeField] private LayerMask enemyLayer;
    private Animator animator;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectileSpawner;
    [SerializeField] private float shootCooldown;
    private float shootTime;

    private void Start() {
        input.AttackEvent += HandleAttack;
        input.RangedAttackEvent += HandleRangedAttack;
        animator = GetComponent<Animator>();
    }

    private void Update() {
        shootCooldown += Time.deltaTime;
    }

    private void HandleAttack() {
        if (Time.time < timeSinceLastAttack) {
            // TODO - Play cooldown sound?
            return;
        }

        animator.SetTrigger("Attack");
        var enemyCollidersHit = Physics2D.OverlapCircleAll(pointOfAttack.position, attackArea, enemyLayer);
        timeSinceLastAttack = Time.time + secondsPerAttack;

        foreach (var collider in enemyCollidersHit) {
            if (collider.isTrigger) {
                continue;
            }

            var healthComponent = collider.GetComponent<EnemyHealth>();
            healthComponent.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(pointOfAttack.position, attackArea);
    }

    private void OnDestroy() {
        input.AttackEvent -= HandleAttack;
        input.RangedAttackEvent -= HandleRangedAttack; 
    }

    /// <summary>
    /// only allow to shoot projectiles at an interval of time away from each other
    /// </summary>
    private void HandleRangedAttack() {
        if (shootCooldown >= shootTime) {
            FireProjectile();
            shootCooldown = 0;
        }
    }
    /// <summary>
    /// instantiate projectile at the position of a child transform on the player and the rotation of the player
    /// </summary>
    private void FireProjectile() {
        Instantiate(projectile, projectileSpawner.position, transform.rotation);
    }
}