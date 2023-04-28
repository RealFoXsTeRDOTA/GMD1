using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void NewGame()
  {
    var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    GameSaver.SaveData(currentSceneIndex, 0);
    SceneManager.LoadSceneAsync(currentSceneIndex + 1);
  }

  public void Continue()
  {
    var savedData = GameSaver.LoadData();
    SceneManager.LoadSceneAsync(savedData.Level);

    var gameController = GameObject.FindGameObjectWithTag("GameController")
      .GetComponent<GameController>();
    gameController.GiveHealth(gameController.MaxPlayerHealth);
    gameController.SetScore(savedData.Collectibles);
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
