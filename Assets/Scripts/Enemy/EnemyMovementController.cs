using UnityEngine;

public class EnemyMovementController : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool moveUnrestricted;
    [SerializeField] private int tileNumber;
    [SerializeField] private bool moveDirectionLeft;

    private Vector2 moveDirection;
    private Vector2 currentPosition;
    private Vector2 initialPosition;
    private Vector2 target;

    private Rigidbody2D enemy;
    private SpriteRenderer enemySprite;
 

    // Start is called before the first frame update
    void Start() {
        enemy = GetComponent<Rigidbody2D>();
        enemy.freezeRotation = true;
        moveDirection = moveDirectionLeft ? Vector2.left : Vector2.right;
        moveDirection.Normalize();
        enemySprite = GetComponent<SpriteRenderer>();
        initialPosition = currentPosition = enemy.position;
        target = Vector2.zero;
    }

    void FixedUpdate() {
        if (!moveUnrestricted) {
            ChangeDirectionForFixedDirection();
        }
        else {
            ReturnToInitialPosition();
        }
        if (!target.Equals(Vector2.zero)) {
            MoveToPlayer();
        }
        else 
            enemy.MovePosition(enemy.position + moveSpeed * Time.fixedDeltaTime * moveDirection);
    }

    private void MoveToPlayer() {
        if ((target.x < enemy.position.x && moveDirection.x > 0) ||
            (target.x > enemy.position.x && moveDirection.x < 0)) {
            FlipMovementDirection();
        }
        enemy.position = Vector2.MoveTowards(enemy.position, target, moveSpeed * Time.fixedDeltaTime);
    }

    private void ChangeDirectionForFixedDirection() {
        if (enemy.position.x > tileNumber + currentPosition.x ||
            enemy.position.x < currentPosition.x - tileNumber) {
            FlipMovementDirection();
            currentPosition = enemy.position;
        }
    }

    //if movement is restricted and the enemy is further (tile + startPosition || startPosition - tile) away, move back to start position and reset movement
    private void ReturnToInitialPosition() {
        if (currentPosition.x > tileNumber + initialPosition.x && moveDirection.x > 0 ||
            currentPosition.x < initialPosition.x - tileNumber && moveDirection.x < 0) {
            enemy.position = Vector2.MoveTowards(enemy.position, initialPosition, moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (moveUnrestricted) {
            if (!(col.gameObject.tag.Equals("Ground") || col.gameObject.tag.Equals("Player"))) {
                FlipMovementDirection();
            }
        }
    }

    //updates each frame so even if the player jumps over the enemy, it will switch direction
    private void OnTriggerStay2D(Collider2D other) {
        if (!other.gameObject.tag.Equals("Player"))
            return;
        var position = other.gameObject.transform.position;
        //set the position to flamingo.y so that the enemy cannot jump after the player
        target = new Vector2(position.x, enemy.position.y);
        Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(),GetComponent<Collider2D>());
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag.Equals("Player"))
            target = Vector2.zero;
    }

    private void FlipMovementDirection() {
        enemySprite.flipX = !enemySprite.flipX;
        moveDirection.x *= -1;
    }
}