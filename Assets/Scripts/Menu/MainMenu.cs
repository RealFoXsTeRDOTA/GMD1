using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  private const string startScene = "Scenes/Tutorial/Level 1";

  public void PlayGame()
  {
    SceneManager.LoadScene(startScene);
  }

  public void QuitGame()
  {
    Debug.Log("Quitting Game...");
    Application.Quit();
  }
}
