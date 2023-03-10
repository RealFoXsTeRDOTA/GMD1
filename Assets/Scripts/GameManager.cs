using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  private InputReader input;

  [SerializeField]
  private GameObject pauseMenu;

  private void Start()
  {
    input.PauseEvent += HandlePause;
    input.ResumeEvent += HandleResume;
  }

  private void HandlePause()
  {

  }

  private void HandleResume()
  {

  }

  private void OnDestroy()
  {
    input.PauseEvent -= HandlePause;
    input.ResumeEvent -= HandleResume;
  }
}
