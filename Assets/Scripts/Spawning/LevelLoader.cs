using UnityEngine;

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

    var gameController = FindFirstObjectByType<GameController>();
    gameController.SpawnPosition = spawnPoint;
    gameController.LoadScene(sceneToLoad);
  }
}
