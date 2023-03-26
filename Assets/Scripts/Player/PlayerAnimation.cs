using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

  [SerializeField]
  private Animator animator;
  private Rigidbody2D body;
  private PlayerController controller;

  private void Start()
  {
    body = GetComponent<Rigidbody2D>();
    controller = GetComponent<PlayerController>();
  }

  private void Update()
  {
    animator.SetFloat("Speed", Mathf.Abs(controller.MoveDirection.x));
    animator.SetFloat("Velocity", body.velocity.y);
  }

  public void SetHit(bool hit)
  {
    animator.SetBool("Hit", hit);
  }

  public void SetDeath(bool death)
  {
    animator.SetBool("Death", death);
  }
}
