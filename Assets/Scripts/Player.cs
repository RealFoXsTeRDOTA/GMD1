using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField]
  private float moveSpeed = 10f;

  [SerializeField]
  private GameInput gameInput;

  private void Update()
  {
    var inputVector = gameInput.GetMovementVectorNormalized();
    var moveDirection = new Vector3(inputVector.x, 0f, 0f);
    transform.position += moveSpeed * Time.deltaTime * moveDirection;
  }
}
