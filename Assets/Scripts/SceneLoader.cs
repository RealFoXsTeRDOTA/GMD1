using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  [SerializeField]
  private Animator animator;

  public void LoadNextScene()
  {
    var triggerName = "FadeOut";
    animator.SetTrigger(triggerName);
  }

  public void OnFadeComplete()
  {
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
  }
}
