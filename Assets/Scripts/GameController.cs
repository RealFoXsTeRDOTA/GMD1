using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private GameObject pauseMenu;

  [SerializeField]
  private TextMeshProUGUI scoreText;

  [SerializeField]
  private Animator animator;
  private string sceneToLoad;

  public int SpawnPosition { get; set; }

  public int Score { get; private set; }

  private void Awake()
  {
    input.PauseEvent += HandlePause;
    input.ResumeEvent += HandleResume;

    Score = 0;
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

  public void LoadScene(string sceneName)
  {
    sceneToLoad = sceneName;
    var triggerName = "FadeOut";
    animator.SetTrigger(triggerName);
  }

  public void OnFadeComplete()
  {
    SceneManager.LoadScene(sceneToLoad);
  }

  private void OnDestroy()
  {
    input.PauseEvent -= HandlePause;
    input.ResumeEvent -= HandleResume;
  }
}
