using UnityEngine;

public class PlayerProjectileController : MonoBehaviour{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D projectile;

    // Start is called before the first frame update
    void Start() {
        projectile = GetComponent<Rigidbody2D>();
        //set to minus to compensate for the rotation of the transform in PlayerController.cs
        projectile.velocity = -transform.right * moveSpeed;
    }

    /// <summary>
    /// Destroy after 1.5s if hits nothing
    /// </summary>
    private void Update() {
        Destroy(gameObject,1.5f);
    }

    /// <summary>
    /// Deal damage to enemy if hit and ignore collision with player and other projectiles
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Projectile") ) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        if (other.gameObject.TryGetComponent(out EnemyHealth enemyHealth)) {
            enemyHealth.TakeDamage(1);
        }
        if (!other.gameObject.CompareTag("Player"))
            Destroy(transform.gameObject);
    }

}