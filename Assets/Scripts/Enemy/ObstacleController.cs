using System;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    [SerializeField] private int damageOnHit;
    [SerializeField] private float timeBetweenHits;
    private float hitCooldown;

    private void Start() {
        hitCooldown = 0;
    }

    private void Update() {
        hitCooldown += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent(out Health playerHealth)) {
                playerHealth.TakeDamage(damageOnHit);
                hitCooldown = 0;
        } 
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.TryGetComponent(out Health playerHealth)) {
            if (hitCooldown >= timeBetweenHits) {
                playerHealth.TakeDamage(damageOnHit);
                hitCooldown = 0;
            }
        } 
    }
}
