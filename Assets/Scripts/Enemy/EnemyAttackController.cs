using UnityEngine;

public class EnemyAttackController : MonoBehaviour {

    [SerializeField] private GameObject projectile;
    private Rigidbody2D enemy;
    private float timer;
    
    // Start is called before the first frame update
    void Start() {
        enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
       
    }

    private void FireProjectile() {
        Instantiate(projectile, enemy.position, Quaternion.identity);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (!other.gameObject.tag.Equals("Player"))
            return;
        timer += Time.deltaTime;
        if (timer > 1) {
            timer = 0;
            FireProjectile();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // timer = 0;
    }

}
