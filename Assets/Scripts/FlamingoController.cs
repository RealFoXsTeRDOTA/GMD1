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
    void FixedUpdate() {
        flamingo.MovePosition(flamingo.position + moveSpeed * Time.fixedDeltaTime * moveDirection);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (!(col.gameObject.tag.Equals("Ground") || col.gameObject.tag.Equals("Player"))) {
            FlipMovementDirection();
        }
    }

    //updates each frame so even if the player jumps over the enemy, it will switch direction
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player")) {
            var position = other.gameObject.transform.position;
            //set the position to flamingo.y so that the enemy cannot jump after the player
            var target = new Vector2(position.x, flamingo.position.y);
            if ((target.x < flamingo.position.x && moveDirection.x > 0) || (target.x > flamingo.position.x && moveDirection.x < 0)) {
                FlipMovementDirection();
            }
            flamingo.position = Vector2.MoveTowards(flamingo.position, target, moveSpeed * Time.fixedDeltaTime);
        }
    }
    
    private void FlipMovementDirection() {
        flamingoSprite.flipX = !flamingoSprite.flipX;
        moveDirection.x *= -1;
    }
}
