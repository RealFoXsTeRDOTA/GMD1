using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  [SerializeField]
  private Animator animator;

  private int sceneIndexToLoad;
  private const string triggerName = "FadeOut";
  private bool isRespawn;

  public void LoadNextScene()
  {
    isRespawn = false;
    sceneIndexToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    animator.SetTrigger(triggerName);
  }

  public void LoadScene(int sceneIndex)
  {
    isRespawn = true;
    sceneIndexToLoad = sceneIndex;
    animator.SetTrigger(triggerName);
  }

  protected void OnFadeComplete()
  {
    SceneManager.LoadSceneAsync(sceneIndexToLoad);
    if (isRespawn)
    {
      FindAnyObjectByType<GameController>().RespawnPlayer();
    }
  }
}
