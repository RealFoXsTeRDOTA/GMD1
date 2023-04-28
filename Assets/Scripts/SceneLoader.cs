using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  [SerializeField]
  private Animator animator;

  private int sceneIndexToLoad;
  private const string triggerName = "FadeOut";

  public void LoadNextScene()
  {
    sceneIndexToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    animator.SetTrigger(triggerName);
  }

  public void LoadScene(int sceneIndex)
  {
    sceneIndexToLoad = sceneIndex;
    animator.SetTrigger(triggerName);
  }

  public void OnFadeComplete()
  {
    SceneManager.LoadSceneAsync(sceneIndexToLoad);
  }
}
