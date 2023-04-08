using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAttackController : MonoBehaviour {

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectilePos;
    private float timer;

    /// <summary>
    /// Instantiate projectile at the position of the spawner inherited from the enemy
    /// </summary>
    private void FireProjectile() {
        Instantiate(projectile, projectilePos.position, Quaternion.identity);
    }

    /// <summary>
    /// Start firing once the player enters the trigger area
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col) {
        if (!col.gameObject.CompareTag("Player"))
            return;
        FireProjectile();
    }

    /// <summary>
    /// Fire a projectile every 0.75 seconds while the player is within range
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other) {
        if (!other.gameObject.CompareTag("Player"))
            return;
        timer += Time.deltaTime;
        if (timer > 0.75) {
            timer = 0;
            FireProjectile();
        }
    }

}
