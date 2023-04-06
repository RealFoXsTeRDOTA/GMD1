using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private Transform attackPoint;

  private void Start()
  {
    input.AttackEvent += HandleAttack;
  }

  private void HandleAttack()
  {
    Debug.Log("Attacking!");
  }

  private void OnDestroy()
  {
    input.AttackEvent -= HandleAttack;
  }
}
