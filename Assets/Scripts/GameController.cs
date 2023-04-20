using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI scoreText;

  [SerializeField]
  private Animator animator;
  private string sceneToLoad;

  public int SpawnPosition { get; set; }

  public int Score { get; private set; }

  private void Awake()
  {
    Score = 0;
  }

  public void IncreaseScore()
  {
    Score++;
    scoreText.text = Score.ToString();
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
}
