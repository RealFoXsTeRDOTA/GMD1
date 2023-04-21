using UnityEngine;

public class Portal : MonoBehaviour
{
  [SerializeField]
  private string sceneToLoad;
  private AudioSource audioSource;

  private void Awake()
  {
    audioSource = GetComponent<AudioSource>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Player"))
    {
      return;
    }

    audioSource.Play();
    var sceneLoader = FindFirstObjectByType<SceneLoader>();
    sceneLoader.LoadScene(sceneToLoad);
  }
}
