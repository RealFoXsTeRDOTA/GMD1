using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  [SerializeField]
  private Animator animator;
  private string sceneToLoad;

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
