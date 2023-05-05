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


    // Start is called before the first frame update
    void Start() {
        enemy = GetComponent<Rigidbody2D>();
        enemy.freezeRotation = true;
        moveDirection = moveDirectionLeft ? Vector2.left : Vector2.right;
        moveDirection.Normalize();
        initialPosition = currentPosition = enemy.position;
        target = Vector2.zero;
        //set the rigidbody so that the player cannot push it around
        enemy.bodyType = moveSpeed == 0 ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
    }

    /// <summary>
    /// Handles movement
    /// Special conditions:
    /// -> If there is a target(player is in the trigger collider), move towards the player
    /// -> Move back to initial position if the movement is restricted to a number of tiles and the enemy went past allowed distance and resume normal movement behaviour
    /// </summary>
    void FixedUpdate() {
        if(moveSpeed != 0) {
            ChangeDirectionForFixedDirection();
            if (!target.Equals(Vector2.zero)) {
                MoveToPlayer();
            }
            else if (!moveUnrestricted && (enemy.position.x > tileNumber + initialPosition.x && moveDirection.x > 0 ||
                                           enemy.position.x < initialPosition.x - tileNumber && moveDirection.x < 0)) {
                enemy.position = Vector2.MoveTowards(enemy.position, initialPosition, moveSpeed * Time.fixedDeltaTime);
                FlipMovementDirection();
            }
            else {
                enemy.MovePosition(enemy.position + moveSpeed * Time.fixedDeltaTime * moveDirection);
            }
        }
    }

    /// <summary>
    /// Moves to towards the player when it is within trigger area
    /// </summary>
    private void MoveToPlayer() {
        if ((target.x < enemy.position.x && moveDirection.x > 0) ||
            (target.x > enemy.position.x && moveDirection.x < 0)) {
            FlipMovementDirection();
        }
        enemy.position = Vector2.MoveTowards(enemy.position, target, moveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// if the enemy can only move back and forth for a couple of tiles then flip the movement direction and set the current position
    /// when maximum distance has been covered in one direction
    /// </summary>
    private void ChangeDirectionForFixedDirection() {
        if (!moveUnrestricted && (enemy.position.x >= tileNumber + currentPosition.x ||
                                  enemy.position.x <= currentPosition.x - tileNumber)) {
            FlipMovementDirection();
            currentPosition = enemy.position;
        }
    }

    /// <summary>
    /// if the enemy can move without restrictions on the entire scene, flip the direction and sprite on collisions (walls, tiles, other obstacles)
    /// </summary>
    /// <param name="col"></param>
    private void OnCollisionEnter2D(Collision2D col) {
        if (moveUnrestricted) {
            if (!(col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Player"))) {
                FlipMovementDirection();
            }
        }
        if (col.gameObject.CompareTag("Enemy")) {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    //updates each frame so even if the player jumps over the enemy, it will switch direction
    private void OnTriggerStay2D(Collider2D other) {
        if (!other.gameObject.CompareTag("Player"))
            return;
        if (moveSpeed == 0) {
            transform.localScale = other.transform.position.x < transform.position.x ? new Vector3(-1, 1) : new Vector3(1, 1);
        }
        else {
            var position = other.gameObject.transform.position;
            //set the position to enemy.y so that the enemy cannot jump after the player
            target = new Vector2(position.x, enemy.position.y);
            //prevents the player from pushing/being pushed by the player
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player"))
            target = Vector2.zero;
    }

    private void FlipMovementDirection() {
        transform.Rotate(0,180f, 0);
        moveDirection.x *= -1;
    }
}