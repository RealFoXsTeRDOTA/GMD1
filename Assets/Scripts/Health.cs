using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Health : MonoBehaviour
{
    [SerializeField] 
    private Image[] hitPoints;
    private PlayerAnimation animationScript;
    private int health;
    private int maxHealth;
    private bool isHit;

    private void Start()
    {
        maxHealth = 9;
        health = 9;
        animationScript = GetComponent<PlayerAnimation>();
    }

    public void Update()
    {
        for (var i = 0; i < maxHealth; i++)
        {
            hitPoints[i].enabled = i < health;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isHit)
        {
            health -= damage;
            StartCoroutine(BecomeTemporarilyInvincible());
            if (health <= 0)
            {
                animationScript.SetDeath(true);
            }
        }
    }
    private IEnumerator BecomeTemporarilyInvincible()
    {
        isHit = true;
        animationScript.SetHit(true);
        yield return new WaitForSeconds(0.25f);
        animationScript.SetHit(false);
        isHit = false;
    }
}
