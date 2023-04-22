using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private InputReader input;

    private void Start()
    {
        input.PauseEvent += Pause;
        input.ResumeEvent += Resume;
    }

    public void Resume()
    {
        Debug.Log("resuming?");
        pauseMenuUI.SetActive(false);
        input.ResumeEvent?.Invoke();
        Time.timeScale = 1f;
    }
    
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
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
