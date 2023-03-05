using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private float moveSpeed;

  [SerializeField]
  private float jumpForce;

  private Vector2 moveDirection;
  private Rigidbody2D body;

  private void Start()
  {
    input.MoveEvent += HandleMove;
    input.JumpEvent += HandleJump;
    input.DescendEvent += HandleDescend;
    input.AttackEvent += HandleAttack;

    body = GetComponent<Rigidbody2D>();
    body.freezeRotation = true;
  }

  private void Update()
  {
    transform.position += moveSpeed * Time.deltaTime * new Vector3(moveDirection.x, 0f, 0f);
  }

  private void HandleMove(Vector2 direction)
  {
    moveDirection = direction;
  }

  private void HandleJump()
  {
    body.velocity = new Vector2(0f, 1f * jumpForce);
  }

  private void HandleDescend()
  {
    Debug.Log("Descend...");
  }

  private void HandleAttack()
  {
    Debug.Log("Avada Kedavra!");
  }
}
