using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
  [SerializeField]
  private string sceneToLoad;

  [SerializeField]
  [Range(0, 10)]
  private int spawnPoint;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Player"))
    {
      return;
    }

    var gameController = collision.gameObject.GetComponent<GameController>();
    gameController.SpawnPosition = spawnPoint;
    SceneManager.LoadScene(sceneToLoad);
  }
}
