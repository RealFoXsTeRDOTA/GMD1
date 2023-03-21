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

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.tag.Equals("Ice") && catMaterialPhysics != null)
    {
      isOnIce = true;
      body.drag = 0f;
      body.sharedMaterial = catMaterialPhysics;
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    Debug.Log("OnTriggerExit2D");
      isOnIce = false;
      body.drag = 4f;
      body.sharedMaterial = null;
  }
}
