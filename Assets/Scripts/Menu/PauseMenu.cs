using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  [SerializeField]
  private GameObject pauseMenuUI;

  [SerializeField]
  private InputReader input;

  public event Action ResumeClickedEvent;

  private void Start()
  {
    input.PauseEvent += Pause;
    input.ResumeEvent += Resume;
  }

  public void Resume()
  {
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    ResumeClickedEvent?.Invoke();
  }

  private void Pause()
  {
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
  }

  public void LoadMenu()
  {
    Time.timeScale = 1f;
    ResumeClickedEvent?.Invoke();
    SceneManager.LoadScene(0);
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  private void OnDestroy()
  {
    input.PauseEvent -= Pause;
    input.ResumeEvent -= Resume;
  }
}
