using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  [SerializeField]
  private GameObject pauseMenuUI;

  [SerializeField]
  private InputReader input;

  public static event Action ResumeClickedEvent;

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
    Debug.Log("Ola");
    Time.timeScale = 1f;
    ResumeClickedEvent?.Invoke();
    SceneManager.LoadScene("MenuScene");
    Debug.Log("Loading Menu...");
  }

  public void QuitGame()
  {
    Debug.Log("Quitting Game...");
    Application.Quit();
  }

  private void OnDestroy()
  {
    input.PauseEvent -= Pause;
    input.ResumeEvent -= Resume;
  }
}
