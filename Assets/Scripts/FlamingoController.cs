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
        moveDirection = Vector2.left;
        moveDirection.Normalize();
        flamingoSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
        
    }

    void FixedUpdate() {
        flamingo.MovePosition(flamingo.position + moveSpeed * Time.fixedDeltaTime * moveDirection);
        // flamingo.velocity += new Vector2(moveSpeed * Time.deltaTime * moveDirection.x, flamingo.velocity.y);
        Debug.Log(Time.fixedDeltaTime +" "+ moveSpeed +" "+ moveDirection  + " "+ flamingo.position);
        // transform.position += moveSpeed * Time.fixedDeltaTime * new Vector3(moveDirection.x,0f,0f);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (!col.gameObject.tag.Equals("Ground")) {
            flamingoSprite.flipX = !flamingoSprite.flipX;
            moveDirection.x *= -1;
        }
    }
}
