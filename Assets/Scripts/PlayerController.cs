using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  //[Header("General settings")]
  [SerializeField]
  private PhysicsMaterial2D catMaterialPhysics;

  private Rigidbody2D body;
  private bool isOnIce = false;
  private SpriteRenderer spriteRenderer;

  [Header("Movement settings")]
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private float moveSpeed;

  [SerializeField]
  private float jumpForce;

  public Vector2 MoveDirection { get; private set; }
  private Vector2 faceDirection;

  [Header("Dash settings")]
  [SerializeField]
  private float dashForce = 24f;

  [SerializeField]
  private float dashCooldown = 1.5f;

  private float dashTime = 0.25f;
  private bool canDash = true;
  private bool isDashing;

  private void Start()
  {
    input.MoveEvent += HandleMove;
    input.JumpEvent += HandleJump;
   // input.DashEvent += HandleDash;

    body = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    faceDirection = Vector2.left;
  }

  private void FixedUpdate()
  {
    if (isDashing)
    {
      return;
    }
    
    if (isOnIce)
    {
      float horizontalInput = Input.GetAxis("Horizontal");
      MoveDirection.Set(horizontalInput, 0f);
      MoveDirection.Normalize();
      body.AddForce(MoveDirection * moveSpeed, ForceMode2D.Force);
    }
    else
    {
      body.velocity = new Vector2(MoveDirection.x * moveSpeed, body.velocity.y);
    }

  }

  private void HandleMove(Vector2 direction)
  {
    MoveDirection = direction;
    if (MoveDirection.x != 0f)
    {
      faceDirection = MoveDirection.normalized;
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
    else
    {
      Debug.Log("Dash is on cooldown!");
      // TODO - Play a sound or something maybe?
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
    //input.DashEvent -= HandleDash;
  }

  private bool IsPlayerGrounded()
  {
    return body.velocity.y <= .2f && body.velocity.y >= -.2f;
  }
}