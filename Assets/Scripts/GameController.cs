using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public int MaxPlayerHealth { get; } = 9;
  public int CurrentPlayerHealth { get; private set; }
  public int Score { get; private set; }

  public event Action<int> ScoreChangedEvent;
  public event Action<int> HealthChangedEvent;
  public event Action PlayerDeathEvent;

  private void Awake()
  {
    CurrentPlayerHealth = MaxPlayerHealth;
    Score = 0;
  }

  public void IncreaseScore()
  {
    Score++;
    ScoreChangedEvent?.Invoke(Score);
  }
  
  public void SetScore(int score)
  {
    Score = score;
    ScoreChangedEvent?.Invoke(Score);
  }

  public void TakeDamage(int damage)
  {
    CurrentPlayerHealth -= damage;
    HealthChangedEvent?.Invoke(CurrentPlayerHealth);

    if (CurrentPlayerHealth == 0)
    {
      PlayerDeathEvent?.Invoke();
    }
  }

  public void GiveHealth(int health)
  {
    CurrentPlayerHealth += health;
    HealthChangedEvent?.Invoke(CurrentPlayerHealth);
  }
}
