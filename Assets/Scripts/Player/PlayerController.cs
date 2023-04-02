using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Rigidbody2D body;
  private SpriteRenderer spriteRenderer;
  private bool isSlipperyMovement = false;

  [Header("Movement settings")]
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private float moveSpeed = 10f;

  [SerializeField]
  private float jumpForce = 19f;
  public Vector2 CurrentMoveDirection { get; private set; }
  private Vector2 faceDirection;

  [Header("Dash settings")]
  [SerializeField]
  private float dashForce = 32f;

  [SerializeField]
  private float dashCooldown = 1.5f;
  private readonly float dashTime = 0.25f;
  private bool canDash = true;
  private bool isDashing;

  private void Start()
  {
    input.MoveEvent += HandleMove;
    input.JumpEvent += HandleJump;
    input.DashEvent += HandleDash;

    body = GetComponent<Rigidbody2D>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    faceDirection = Vector2.right;
  }

  private void FixedUpdate()
  {
    if (isDashing)
    {
      return;
    }

    if (isSlipperyMovement && IsPlayerGrounded())
    {
      body.AddForce(new Vector2(CurrentMoveDirection.x * moveSpeed, 0f));
    }
    else if (isSlipperyMovement && !IsPlayerGrounded())
    {
      if (CurrentMoveDirection.x != 0f)
      {
        body.velocity = new Vector2(CurrentMoveDirection.x * moveSpeed, body.velocity.y);
      }
    }
    else
    {
      body.velocity = new Vector2(CurrentMoveDirection.x * moveSpeed, body.velocity.y);
    }
  }

  private void HandleMove(Vector2 direction)
  {
    CurrentMoveDirection = direction;
    if (CurrentMoveDirection.x != 0f)
    {
      faceDirection = CurrentMoveDirection.normalized;
      spriteRenderer.flipX = faceDirection.x > 0f;
    }
  }

  private void HandleJump()
  {
    if (IsPlayerGrounded())
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

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("SlipperySurface"))
    {
      isSlipperyMovement = true;
    }
    else
    {
      isSlipperyMovement = false;
    }
  }

  private bool IsPlayerGrounded()
  {
    return body.velocity.y <= .3f && body.velocity.y >= -.3f;
  }

  private void OnDestroy()
  {
    input.MoveEvent -= HandleMove;
    input.JumpEvent -= HandleJump;
    input.DashEvent -= HandleDash;
  }
}
