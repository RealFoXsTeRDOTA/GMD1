using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
  
    [SerializeField] 
    private Animator animator;
    private Rigidbody2D body;
    
    private void Start()
    {
        body =  GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
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
