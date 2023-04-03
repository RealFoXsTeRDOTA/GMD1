using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  //[Header("General settings")]
  [SerializeField]
  public GameObject fireParticleSystem;

  private TerrainController terrainController;
  private Rigidbody2D body;
  public bool isOnIce = false;
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
    input.DashEvent += HandleDash;

    terrainController = new TerrainController();

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
      body.AddForce(new Vector2(horizontalInput * moveSpeed, 0f), ForceMode2D.Force);
    }
    else
    {
      body.velocity = new Vector2(MoveDirection.x * moveSpeed, body.velocity.y);
    }

  }

  private void HandleMove(Vector2 direction)
  {
    if (direction == Vector2.zero)
    {
      MoveDirection = Vector2.zero;
      return;
    }

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

  // Problem with OnCollisionEnter and OnCollisionExit that I noticed on the 'fix-slippery-surface' branch
  // is that when the tiles are actually 16x16, when the player (who is 32x16) reaches the border of two tilemaps,
  // you are in the state where you collide with 2 different ones. And in the case of the ice to normal border, if you slide off the ice onto normal tiles
  // and then immediately go back on ice without actually exiting the ice collider, you are no longer considered "on ice" because you entered normal tile but didn't exit ice, hence you didn't enter ice again either
  // and this is why I defaulted to use OnCollisionStay. Is it a great way to do it? Probably not :/ But I'm not sure how else you would combat this issue considering the player is twice as wide as one tile.
  private void OnCollisionEnter2D(Collision2D col)
  {
    var tile = col.gameObject.GetComponent<ITile>();
    tile?.OnEnter(this);
  }

  private void OnCollisionExit2D(Collision2D collision)
  {
    var tile = collision.gameObject.GetComponent<ITile>();
    tile?.OnExit(this);
  }


  private void OnDestroy()
  {
    input.MoveEvent -= HandleMove;
    input.JumpEvent -= HandleJump;
    input.DashEvent -= HandleDash;
  }

  private bool IsPlayerGrounded()
  {
    return body.velocity.y <= .2f && body.velocity.y >= -.2f;
  }
}
