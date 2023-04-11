using System.Collections;
using Tiles;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private Rigidbody2D body;
  public bool IsSlipperyMovement { get; set; }

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

  [Header("SFX")]
  private AudioSource audioSource;

  [SerializeField]
  private AudioClip dashSoundEffect;

  [SerializeField]
  private AudioClip dashCooldownSoundEffect;

  private void Start()
  {
    input.MoveEvent += HandleMove;
    input.JumpEvent += HandleJump;
    input.DashEvent += HandleDash;

    body = GetComponent<Rigidbody2D>();
    audioSource = GetComponent<AudioSource>();
    faceDirection = Vector2.right;
    FlipCharacter();
  }

  private void FixedUpdate()
  {
    if (isDashing)
    {
      return;
    }

    if (IsSlipperyMovement && IsPlayerGrounded())
    {
      body.AddForce(new Vector2(CurrentMoveDirection.x * moveSpeed, 0f));
    }
    else if (IsSlipperyMovement && !IsPlayerGrounded())
    {
      if (CurrentMoveDirection.x == 0f)
      {
        return;
      }

      body.velocity = new Vector2(CurrentMoveDirection.x * moveSpeed, body.velocity.y);
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
      if (CurrentMoveDirection.x < 0f && faceDirection.x > 0f)
      {
        FlipCharacter();
      }
      else if (CurrentMoveDirection.x > 0f && faceDirection.x < 0f)
      {
        FlipCharacter();
      }

      faceDirection = CurrentMoveDirection.normalized;
    }
  }

  private void FlipCharacter()
  {
    transform.Rotate(0f, 180f, 0f);
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
      audioSource.PlayOneShot(dashCooldownSoundEffect);
    }
  }

  private IEnumerator Dash()
  {
    canDash = false;
    isDashing = true;
    var gravity = body.gravityScale;
    body.gravityScale = 0f;
    body.AddForce(faceDirection * dashForce, ForceMode2D.Impulse);
    audioSource.PlayOneShot(dashSoundEffect);

    yield return new WaitForSeconds(dashTime);

    body.gravityScale = gravity;
    isDashing = false;

    yield return new WaitForSeconds(dashCooldown);

    canDash = true;
  }

  private void OnCollisionEnter2D(Collision2D col)
  {
    var tile = col.gameObject.GetComponent<IEnterExitTile>();
    tile?.OnEnter(this);
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    var tile = collision.gameObject.GetComponent<IEnterExitTile>();
    tile?.OnExit(this);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    var tile = collision.gameObject.GetComponent<IStayTile>();
    tile?.OnStay(this);
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
