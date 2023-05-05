using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour {
    [SerializeField]
    private float maxHealth = 5;
    private float currentHealth;
    private ParticleSystem particlesOnDamage;
    
    private bool slimeSpawned;
    private bool ghostSpawned;
    [SerializeField] private GameObject slime;
    [SerializeField] private GameObject ghost;
    [SerializeField] private Transform slimeSpawner;
    [SerializeField] private Transform ghostSpawner;

    private GameObject bossHealth;
    private Vector3 healthBarScale;

    [SerializeField] private GameObject portal;
    

    [SerializeField]
    private GameObject particlesOnDeath;

    [SerializeField]
    private AudioClip deathSoundEffect;

    private void Start()
    {
        particlesOnDamage = GetComponent<ParticleSystem>();
        currentHealth = maxHealth;
        slimeSpawned = false;
        ghostSpawned = false;
        bossHealth = GameObject.FindGameObjectWithTag("HealthBar");
        healthBarScale = bossHealth.transform.localScale;
        UpdateBossHealth();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Kill();
        }
        else
        {
            Debug.Log(currentHealth);
            SpawnGhost();
            SpawnSlime();
            UpdateBossHealth();
            particlesOnDamage.Play();
        }
    }

    private void Kill()
    {
        FindFirstObjectByType<AudioManager>().Play(deathSoundEffect);
        Instantiate(particlesOnDeath, transform.position, Quaternion.identity);
        portal.SetActive(true);
        Destroy(gameObject);
    }

    /// <summary>
    /// spawn slime when there is less than 30% health
    /// </summary>
    private void SpawnSlime() {
        if (currentHealth <= maxHealth * 3 / 10 && !slimeSpawned) {
            Instantiate(slime, slimeSpawner.position, transform.rotation);
            slimeSpawned = true;
        }
    }

    /// <summary>
    /// spawn ghost when there is less than 60% health
    /// </summary>
    private void SpawnGhost() {
        if (currentHealth <= maxHealth * 9 / 10 && !ghostSpawned) {
            Instantiate(ghost, ghostSpawner.position, transform.rotation);
            ghostSpawned = true;
        }
    }

    private void UpdateBossHealth() {
        Vector2 scale = new Vector2((currentHealth*healthBarScale.x)/maxHealth, healthBarScale.y);
        bossHealth.transform.localScale = scale;
    }
}