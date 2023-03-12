using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private float moveSpeed;

  [SerializeField]
  private float jumpForce;
  
  [SerializeField] 
  private Animator animator;

  private Vector2 moveDirection;
  private Rigidbody2D body;
  private SpriteRenderer spriteRenderer;

  private void Start()
  {
    input.MoveEvent += HandleMove;
    input.JumpEvent += HandleJump;
    input.DescendEvent += HandleDescend;
    input.AttackEvent += HandleAttack;

    body = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void FixedUpdate()
  {
    body.velocity = new Vector2(moveDirection.x * moveSpeed, body.velocity.y);
    animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    animator.SetFloat("Velocity", body.velocity.y);
  }

  private void HandleMove(Vector2 direction)
  {
    moveDirection = direction;
    if (moveDirection.x != 0f)
    {
      spriteRenderer.flipX = moveDirection.x > 0f;
    }
  }

  private void HandleJump()
  {
    if (IsPlayerGrounded())
    {
      body.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }
  }

  private void HandleDescend()
  {
    Debug.Log("Descend...");
  }

  private void HandleAttack()
  {
    Debug.Log("Avada Kedavra!");
  }

  private void OnDestroy()
  {
    input.MoveEvent -= HandleMove;
    input.JumpEvent -= HandleJump;
    input.DescendEvent -= HandleDescend;
    input.AttackEvent -= HandleAttack;
  }

  private bool IsPlayerGrounded()
  {
    return body.velocity.y == 0f;
  }
}
