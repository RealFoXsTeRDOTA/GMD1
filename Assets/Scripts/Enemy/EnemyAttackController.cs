using UnityEngine;

public class EnemyAttackController : MonoBehaviour {
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectilePos;
    private float timer;
    private Vector3 target;
    [SerializeField] private float projectileFrequency;

    private void Start() {
        target = Vector3.zero;
    }

    /// <summary>
    /// Fire a projectile every 0.75 seconds while the player is within range
    /// </summary>
    /// <param name="other"></param>
    private void FixedUpdate() {
        if (!target.Equals(Vector2.zero)) {
            timer += Time.deltaTime;
            if (timer > projectileFrequency) {
                timer = 0;
                FireProjectile();
            }
        }
    }

    /// <summary>
    /// Instantiate projectile at the position of the spawner inherited from the enemy
    /// </summary>
    private void FireProjectile() {
        Instantiate(projectile, projectilePos.position, Quaternion.identity);
    }
    
    /// <summary>
    /// Set the target every time it moves inside the trigger area
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("Player"))
            return;
        target = other.transform.position;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
            target = Vector2.zero;
    }
}