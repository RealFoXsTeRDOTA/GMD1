using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private GameObject pauseMenu;

  [SerializeField]
  private TextMeshProUGUI scoreText;

  public int Score { get; private set; }

  private void Awake()
  {
    input.PauseEvent += HandlePause;
    input.ResumeEvent += HandleResume;

    Score = 0;

    DontDestroyOnLoad(gameObject);
  }

  public void IncreaseScore()
  {
    Score++;
    scoreText.text = Score.ToString();
  }

  private void HandlePause()
  {

  }

  private void HandleResume()
  {

  }

  private void OnDestroy()
  {
    input.PauseEvent -= HandlePause;
    input.ResumeEvent -= HandleResume;
  }
}
