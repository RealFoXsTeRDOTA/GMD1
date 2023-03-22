using UnityEngine;

public class FlamingoController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D flamingo;

    private SpriteRenderer flamingoSprite;
    // Start is called before the first frame update
    void Start() {
        flamingo = GetComponent<Rigidbody2D>();
        flamingo.freezeRotation = true;
        moveDirection = transform.position;
        flamingoSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * new Vector3(moveDirection.x,0f,0f);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag.Equals("Tile")) {
            flamingoSprite.flipX = !flamingoSprite.flipX;
            moveDirection.x *= -1;
        }
    }
}
