using UnityEngine;

public class ProjectileController : MonoBehaviour, IProjectile {
    [SerializeField] private float moveSpeed;
    private GameObject player;
    private Rigidbody2D projectile;
    // Start is called before the first frame update
    void Start() {
        projectile = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        MoveToTarget(player);
    }
    /// <summary>
    /// Projectile will move to the position of where the target was when it was spawned
    /// </summary>
    /// <param name="target"></param>
    public void MoveToTarget(GameObject target) {
        Vector3 direction = target.transform.position - transform.position;
        projectile.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;
    }
    /// <summary>
    /// ignore collision with enemy and destroy projectile upon collision with any object other than the enemy
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag.Equals("Enemy"))
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        if (!col.gameObject.tag.Equals("Enemy"))
            Destroy(transform.gameObject);
    }
}
