using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
  private static GameController instance;
  public int MaxPlayerHealth { get; } = 9;
  public int CurrentPlayerHealth { get; private set; }
  public int Score { get; private set; } = 0;

  public event Action<int> ScoreChangedEvent;
  public event Action<int> HealthChangedEvent;
  public event Action PlayerDeathEvent;
  public event Action PlayerRespawnEvent;

  private void Awake()
  {
    if (instance != null && instance != this)
    {
      Destroy(gameObject);
      return;
    }
    else
    {
      instance = this;
      CurrentPlayerHealth = MaxPlayerHealth;
    }

    DontDestroyOnLoad(gameObject);
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

    if (CurrentPlayerHealth <= 0)
    {
      PlayerDeathEvent?.Invoke();
      var savedData = GameSaver.LoadData();
      SetScore(savedData.Collectibles);
      FindFirstObjectByType<SceneLoader>().LoadScene(savedData.Level);
    }
  }

  public void RespawnPlayer()
  {
    GiveHealth(MaxPlayerHealth);
    PlayerRespawnEvent?.Invoke();
  }

  public void GiveHealth(int health)
  {
    CurrentPlayerHealth += health;

    if (CurrentPlayerHealth > MaxPlayerHealth)
    {
      CurrentPlayerHealth = MaxPlayerHealth;
    }

    HealthChangedEvent?.Invoke(CurrentPlayerHealth);
  }
}
