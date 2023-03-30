using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
  [SerializeField]
  private int sceneToLoad;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      SceneManager.LoadScene(sceneToLoad);
    }
  }
}
