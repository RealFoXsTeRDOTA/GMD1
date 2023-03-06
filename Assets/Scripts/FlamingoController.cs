using UnityEngine;

public class FlamingoController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D flamingo;
    // Start is called before the first frame update
    void Start() {
        flamingo = GetComponent<Rigidbody2D>();
        flamingo.freezeRotation = true;
        moveDirection = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * new Vector3(moveDirection.y,0f , 0f);
    }
}
