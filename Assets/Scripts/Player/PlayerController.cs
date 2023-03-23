using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  //[Header("General settings")]
  //[SerializeField]
  //private PhysicsMaterial2D catMaterialPhysics;

  private Rigidbody2D body;
  //private bool isOnIce = false;
  private SpriteRenderer spriteRenderer;

  [Header("Movement settings")]
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private float moveSpeed;

  [SerializeField]
  private float jumpForce;

  private Vector2 moveDirection;
  private Vector2 faceDirection;

  [Header("Dash settings")]
  [SerializeField]
  private float dashForce = 24f;

  [SerializeField]
  private float dashTime = 0.25f;

  [SerializeField]
  private float dashCooldown = 1.5f;

  private bool canDash = true;
  private bool isDashing;

  private void Start()
  {
    input.MoveEvent += HandleMove;
    input.JumpEvent += HandleJump;
    input.DashEvent += HandleDash;

    body = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    faceDirection = Vector2.left;
  }

  private void Update()
  {
    //if (isOnIce)
    //{
    //  float horizontalInput = Input.GetAxis("Horizontal");
    //  moveDirection = new Vector2(horizontalInput, 0f).normalized;
    //}
    //else
    //{
    //  transform.position += moveSpeed * Time.deltaTime * new Vector3(moveDirection.x, 0f, 0f);
    //}
  }

  private void FixedUpdate()
  {
    if (isDashing)
    {
      return;
    }

    body.velocity = new Vector2(moveDirection.x * moveSpeed, body.velocity.y);
  }

  private void HandleMove(Vector2 direction)
  {
    moveDirection = direction;
    if (moveDirection.x != 0f)
    {
      faceDirection = moveDirection.normalized;
      spriteRenderer.flipX = faceDirection.x > 0f;
    }
  }

  private void HandleJump()
  {
    if (IsPlayerGrounded() && !isDashing)
    {
      body.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }
  }

  private void HandleDash()
  {
    if (canDash)
    {
      StartCoroutine(Dash());
    }
  }

  private IEnumerator Dash()
  {
    canDash = false;
    isDashing = true;
    var gravity = body.gravityScale;
    body.gravityScale = 0f;
    body.AddForce(faceDirection * dashForce, ForceMode2D.Impulse);

    yield return new WaitForSeconds(dashTime);

    body.gravityScale = gravity;
    isDashing = false;

    yield return new WaitForSeconds(dashCooldown);

    canDash = true;
  }

  //private void OnTriggerEnter2D(Collider2D col)
  //{

  //  if (col.gameObject.tag.Equals("Ice") && catMaterialPhysics != null)
  //  {
  //    Debug.Log("OnTriggerEnter2D");
  //    isOnIce = true;
  //    body.drag = 0f;
  //    body.sharedMaterial = catMaterialPhysics;
  //  }
  //}

  //private void OnTriggerExit2D(Collider2D collision)
  //{
  //  Debug.Log("OnTriggerExit2D");
  //  isOnIce = false;
  //  body.drag = 4f;
  //  body.sharedMaterial = null;
  //}

  private void OnDestroy()
  {
    input.MoveEvent -= HandleMove;
    input.JumpEvent -= HandleJump;
    input.DashEvent -= HandleDash;
  }

  private bool IsPlayerGrounded()
  {
    return body.velocity.y == 0f;
  }
}
