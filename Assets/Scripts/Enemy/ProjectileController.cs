using UnityEngine;

public class ProjectileController : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    private GameObject player;
    private Rigidbody2D projectile;
    // Start is called before the first frame update
    void Start() {
        projectile = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        projectile.velocity = new Vector2(direction.x, direction.y).normalized * moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    

    private void OnCollisionEnter2D(Collision2D col) {
        if (!col.gameObject.tag.Equals("Player"))
            return;
        Destroy(transform.gameObject);
    }
}
