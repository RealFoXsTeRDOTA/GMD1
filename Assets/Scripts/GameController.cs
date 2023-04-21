using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
  private int playerHealth;
  public int Score { get; private set; }

  public event Action<int> ScoreIncreasedEvent;

  private void Awake()
  {
    playerHealth = 9;
    Score = 0;
  }

  public void IncreaseScore()
  {
    Score++;
    ScoreIncreasedEvent?.Invoke(Score);
  }
}
