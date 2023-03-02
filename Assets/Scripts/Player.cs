using System;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField]
  private float moveSpeed = 10f;

  [SerializeField]
  private float jumpForce = 16f;

  [SerializeField]
  private GameInput gameInput;

  private Rigidbody2D rigidBody;

  private void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
    gameInput.OnJump += OnJump;
  }

  private void Update()
  {
    var inputVector = gameInput.GetMovementVector();
    var moveDirection = new Vector3(inputVector.x, 0f, 0f);
    transform.position += moveSpeed * Time.deltaTime * moveDirection;
  }

  private void OnDestroy()
  {
    gameInput.OnJump -= OnJump;
  }

  private void OnJump(object sender, EventArgs args)
  {
    rigidBody.velocity = new Vector2(0f, 1f * jumpForce);
  }
}
