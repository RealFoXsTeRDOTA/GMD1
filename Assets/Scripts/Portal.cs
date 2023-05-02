using UnityEngine;

public class Portal : MonoBehaviour
{
  [SerializeField]
  private AudioClip portalSound;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (!collision.gameObject.CompareTag("Player"))
    {
      return;
    }

    FindFirstObjectByType<AudioManager>().Play(portalSound);
    FindFirstObjectByType<SceneLoader>().LoadNextScene();
  }
}
