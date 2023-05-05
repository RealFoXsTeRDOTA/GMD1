using TMPro;
using UnityEngine;

public class ScorerUserInterface : MonoBehaviour
{
  private TextMeshProUGUI scoreText;
  private GameController gameController;

  private void Start()
  {
    scoreText = GetComponentInChildren<TextMeshProUGUI>();
    gameController = FindFirstObjectByType<GameController>();

    scoreText.text = gameController.Score.ToString();
    gameController.ScoreChangedEvent += UpdateScore;
  }

  private void UpdateScore(int score)
  {
    scoreText.text = score.ToString();
  }

  private void OnDestroy()
  {
    gameController.ScoreChangedEvent -= UpdateScore;
  }
}
