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
  private PhysicsMaterial2D catMaterialPhysics;

  private Vector2 moveDirection;
  private Rigidbody2D body;
  private bool isOnIce = false;
  private SpriteRenderer spriteRenderer;

  private void Start()
  {
    input.MoveEvent += HandleMove;
    input.JumpEvent += HandleJump;
    input.DescendEvent += HandleDescend;
    input.AttackEvent += HandleAttack;

    body = GetComponent<Rigidbody2D>();
    body.freezeRotation = true;
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void Update()
  {
    if (isOnIce)
    {
      float horizontalInput = Input.GetAxis("Horizontal");
      moveDirection = new Vector2(horizontalInput, 0f).normalized;
    }
    else
    {
      transform.position += moveSpeed * Time.deltaTime * new Vector3(moveDirection.x, 0f, 0f);
    }

  }

  private void FixedUpdate()
  {
    body.AddForce(moveDirection * moveSpeed, ForceMode2D.Force);
//    body.velocity = new Vector2(moveDirection.x * moveSpeed, body.velocity.y);
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
  private void OnCollisionEnter2D(Collision2D col)
  {
    
    if (col.gameObject.tag.Equals("Ice") && catMaterialPhysics != null)
    {
      Debug.Log("OnTriggerEnter2D");
      isOnIce = true;
      body.drag = 0f;
      body.sharedMaterial = catMaterialPhysics;
    }
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    Debug.Log("OnTriggerExit2D");
      isOnIce = false;
      body.drag = 4f;
      body.sharedMaterial = null;
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
