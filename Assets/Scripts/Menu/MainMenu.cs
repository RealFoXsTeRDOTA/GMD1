using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  private GameController gameController;

  private void Awake()
  {
    gameController = FindFirstObjectByType<GameController>();
  }

  public void NewGame()
  {
    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    gameController.SetScore(0);
    GameSaver.SaveData(currentSceneIndex + 1, gameController.Score);
    SceneManager.LoadSceneAsync(currentSceneIndex + 1);
  }

  public void Continue()
  {
    var savedData = GameSaver.LoadData();
    SceneManager.LoadSceneAsync(savedData.Level);
    gameController.GiveHealth(gameController.MaxPlayerHealth);
    gameController.SetScore(savedData.Collectibles);
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
