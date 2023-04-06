using UnityEngine;

public class EnemyAttackController : MonoBehaviour {

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform projectilePoz;
    private float timer;

    private void FireProjectile() {
        Instantiate(projectile, projectilePoz.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (!col.gameObject.tag.Equals("Player"))
            return;
        FireProjectile();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (!other.gameObject.tag.Equals("Player"))
            return;
        timer += Time.deltaTime;
        if (timer > 0.75) {
            timer = 0;
            FireProjectile();
        }
    }

}
