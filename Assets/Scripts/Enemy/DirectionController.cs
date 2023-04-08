
using UnityEngine;

public class DirectionController : MonoBehaviour {
    /// <summary>
    /// Ignore collision with the player 
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.CompareTag("Player")) {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    /// <summary>
    /// update the scale of the ghost to always face the player while it is within trigger area
    /// </summary>
    private void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("Player"))
            return;
        transform.localScale = other.transform.position.x < transform.position.x ? new Vector3(-1, 1) : new Vector3(1, 1);

    }
}