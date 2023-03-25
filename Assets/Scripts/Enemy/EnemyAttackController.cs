using UnityEngine;

public class EnemyAttackController : MonoBehaviour {
    [SerializeField]
    private float maxHealth;
    private float health;

    private bool isHit;
    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
