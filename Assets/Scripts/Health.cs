using System;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Health : MonoBehaviour
{
    [SerializeField] 
    private Image[] hitPoints;
    private int health;
    private const int maxHealth = 9;
    private GameObject player; 

    private void Start()
    {
        health = 9;
        player = GameObject.FindWithTag("Player");
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
        health -= damage;
        if (health <= 0)
        {
            
        }
    }
}
