using UnityEngine;

public class Portal : MonoBehaviour
{
  [SerializeField]
  private string sceneToLoad;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Player"))
    {
      return;
    }

    var sceneLoader = FindFirstObjectByType<SceneLoader>();
    sceneLoader.LoadScene(sceneToLoad);
  }
}
